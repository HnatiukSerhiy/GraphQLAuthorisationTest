using GraphQL.Types;
using Test.Models;

namespace Test.Api.Types
{
    public class LoginUserType : ObjectGraphType<LoginUserResponseModel>
    {
        public LoginUserType()
        {
            Field(user => user.UserName);
            Field(user => user.Password);
            Field(user => user.Token);
        }
    }
}
