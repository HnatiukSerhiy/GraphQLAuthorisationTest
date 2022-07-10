using GraphQL.Types;

namespace Test.Api.Types
{
    public class LoginUserInputType : InputObjectGraphType
    {
        public LoginUserInputType()
        {
            Name = "LoginUserInput";
            Field<NonNullGraphType<StringGraphType>>("username");
            Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}
