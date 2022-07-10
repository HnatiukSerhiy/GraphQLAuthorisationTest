using GraphQL.Types;
using Test.Models;

namespace Test.Api.Types
{
    public class EmployeeType : ObjectGraphType<Employee>
    {
        public EmployeeType()
        {
            Field(emp => emp.Id);
            Field(emp => emp.Name);
            Field(emp => emp.Age);
            Field(emp => emp.JobTittle);
        }
    }
}
