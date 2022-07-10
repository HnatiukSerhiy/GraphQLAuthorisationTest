using GraphQL;
using GraphQL.Authorization;
using GraphQL.MicrosoftDI;
using GraphQL.SystemTextJson;
using GraphQL.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Test.Api;
using Test.Repository;
using GraphQL.Types;
using GraphQL.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddTransient<AuthorizationSettings>();
builder.Services.AddTransient<IValidationRule, AuthorizationValidationRule>();
builder.Services.AddTransient<IAuthorizationEvaluator, AuthorizationEvaluator>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "audience",
        ValidIssuer = "issuer",
        RequireSignedTokens = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"))
    };

    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
});

builder.Services.AddGraphQL(b => b
                .AddHttpMiddleware<ISchema>()
                .AddUserContextBuilder(context => new GraphQLUserContext(context.User))
                .AddValidationRule<AuthorizationValidationRule>()
                .AddSystemTextJson()
                .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = true)
                .AddSchema<AppSchema>()
                .AddGraphTypes(typeof(AppSchema).Assembly))
                .AddAuthorization(policyBuilder =>
                {
                    policyBuilder.AddPolicy("UserPolicy", p => p.RequireClaim("role", "User"));
                });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseGraphQL<ISchema>();

app.UseGraphQLAltair();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();