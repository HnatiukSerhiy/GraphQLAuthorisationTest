using Test.Models;

namespace Test.Repository
{
    public interface IEmployeeRepository
    {
        LoginUserResponseModel Login(LoginUserModel model);
        List<Employee> GetAll();
    }
}
