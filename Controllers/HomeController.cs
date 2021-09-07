using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ATOCalculator.Models;
using ATOCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATOCalculator.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeService employeeService;


        public HomeController(EmployeeService es)
        {
            employeeService = es;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public List<PaySlip> InputEmployee ([FromBody] List<Employee> employees)
        {
            return employeeService.ExportPaySlip(employees);
        }

        [HttpPost]
        public string InputTaxThreshold([FromBody] TaxThreshold jsonInput)
        {
            return employeeService.AssignTaxThreshold(jsonInput);
        }
    }
}
