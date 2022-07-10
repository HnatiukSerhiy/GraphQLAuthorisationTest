using GraphQL.Types;

namespace Test.Api
{
    public class AppSchema : Schema, ISchema
    {
        public AppSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<AppQuery>();
        }
    }
}
