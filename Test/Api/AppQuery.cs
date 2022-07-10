using GraphQL;
using GraphQL.Types;
using Test.Api.Types;
using Test.Models;
using Test.Repository;

namespace Test.Api
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IServiceProvider serviceProvider)
        {
            IEmployeeRepository employeRepository = serviceProvider.GetRequiredService<IEmployeeRepository>();

            Field<LoginUserType>("login",
                arguments: new QueryArguments { new QueryArgument<LoginUserInputType> { Name = "user" } },
                resolve: context =>
                {
                    var user = context.GetArgument<LoginUserModel>("user");
                    return employeRepository.Login(user);
                });

            Field<EmployeeType>("employee",
                resolve: context =>
                {
                    return new Employee
                    {
                        Id = 1,
                        Name = "Oleg",
                        Age = 25,
                        JobTittle = "Programmer"
                    };
                });

            Field<ListGraphType<EmployeeType>>("employers",
                resolve: context =>
                {
                    return employeRepository.GetAll();
                });

            Field<StringGraphType>("hello",
                resolve: context => "Hello world!");

            Field<StringGraphType>("welcome",
                resolve: context => "welcome!").AuthorizeWithPolicy("UserPolicy");

        }
    }
}
