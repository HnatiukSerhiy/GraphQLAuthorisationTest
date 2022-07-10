using GraphQL.Authorization;
using System.Security.Claims;

namespace Test.Api
{
    public class GraphQLUserContext : Dictionary<string, object>, IProvideClaimsPrincipal
    {
        public ClaimsPrincipal? User { get; set; }
        public GraphQLUserContext(ClaimsPrincipal User)
        {
            this.User = User;
        }
    }
}
