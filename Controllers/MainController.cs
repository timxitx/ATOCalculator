using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATOCalculator.Models;
using ATOCalculator.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ATOCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MainController : ControllerBase
    {

        private readonly ILogger<MainController> _logger;
        private readonly DataContext _context;
        private EmployeeService employeeService;

        public MainController(ILogger<MainController> logger, DataContext context, EmployeeService es)
        {
            _logger = logger;
            _context = context;
            employeeService = es;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaxThreshold>> GetTaxThresholds()
        {
            return _context.taxThreshold;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult<IEnumerable<PaySlip>> InputEmployee([FromBody] List<Employee> employees)
        {
            return employeeService.ExportPaySlip(employees, _context);
        }

        [Route("[action]")]
        [HttpPost]
        public string ImportTaxThreshold([FromBody] TaxThreshold taxThreshold)
        {
            return employeeService.AssignTaxThreshold(taxThreshold, _context);
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> AllEmployees()
        {
            return _context.employee.ToList();
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult<IEnumerable<PaySlip>> AllPayslips()
        {
            return _context.payslip.ToList();
        }

        [Route("[action]")]
        [HttpGet]
        public string Clear()
        {
            return employeeService.clearData(_context);
        }
        /*
        [HttpGet]
        public string Get()
        {
            return "Hi this is the home page";
        }
        */
    }
}
