using Microsoft.AspNetCore.Mvc;
using Test.Models;
using Test.Repository;
using System.Xml;
using System.Xml.Serialization;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository employeRepository;

        public HomeController(IEmployeeRepository employeRepository)
        {
            this.employeRepository = employeRepository;
        }

        public ActionResult Index()
        {            
            return View(employeRepository.GetAll());
        }
    }
}