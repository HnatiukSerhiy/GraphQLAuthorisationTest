using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Test.Models;

namespace Test.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public LoginUserResponseModel Login(LoginUserModel model)
        {
            return new LoginUserResponseModel
            {
                UserName = model.UserName,
                Password = model.Password,
                Token = GenerateAccessToken(model)
            };
        }

        public string GenerateAccessToken(LoginUserModel model)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecret"));

            var claims = new List<Claim>
            {
                new Claim("username", model.UserName),
                new Claim("password", model.Password),
                new Claim("role", "User")
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public List<Employee> GetAll()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Id = 1,
                    Name = "Oleg",
                    Age = 25,
                    JobTittle = "Builder"
                },

                new Employee
                {
                    Id = 2,
                    Name = "Yuriy",
                    Age = 20,
                    JobTittle = "Driver"
                },

                new Employee
                {
                    Id = 3,
                    Name = "Mike",
                    Age = 30,
                    JobTittle = "Programmer"
                }
            };
        }
    }
}
