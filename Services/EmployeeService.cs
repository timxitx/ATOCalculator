using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATOCalculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace ATOCalculator.Services
{
    public class EmployeeService
    {
        TaxThreshold taxThreshold;
        int[] taxThresholds;
        double[] taxRates;
        public EmployeeService()
        {
            this.taxThreshold = new TaxThreshold();
        }
        public ActionResult<IEnumerable<PaySlip>> ExportPaySlip(List<Employee> employees, DataContext context)
        {
            taxThresholds = new int[] { -1, this.taxThreshold.Threshold1, this.taxThreshold.Threshold2, this.taxThreshold.Threshold3, this.taxThreshold.Threshold4, int.MaxValue };
            taxRates = new double[] { this.taxThreshold.TaxRate1, this.taxThreshold.TaxRate2, this.taxThreshold.TaxRate3, this.taxThreshold.TaxRate4, this.taxThreshold.TaxRate5 };

            System.Console.WriteLine("Adding employee...");
            foreach (Employee e in employees)
            {
                System.Console.WriteLine("Add Employee...");
                context.employee.Add(e);
                context.SaveChanges();

                PaySlip payslip = new PaySlip();
                payslip.GrossIncome = (int)Math.Round((double)e.AnnualSalary / 12, MidpointRounding.AwayFromZero);
                payslip.IncomeTax = payslip.CalculateIcomeTax(e.AnnualSalary, taxThresholds, taxRates);
                payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                payslip.Superannuation = (int)Math.Round(payslip.GrossIncome * e.SuperRate, MidpointRounding.AwayFromZero);
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, e.paymentMonth, 1);
                payslip.FromDate = firstDayOfMonth.Day.ToString();
                payslip.ToDate = firstDayOfMonth.AddMonths(1).AddDays(-1).Day.ToString();
                payslip.EmployeeId = e.Id;
                payslip.TaxThresholdId = taxThreshold.Id;
                context.payslip.Add(payslip);
                context.SaveChanges();
            }
            
            return context.payslip;
        }

        public string AssignTaxThreshold(TaxThreshold tts, DataContext context)
        {
            System.Console.WriteLine("Add TaxThreshold...");
            context.taxThreshold.Add(tts);
            context.SaveChanges();
            this.taxThreshold = tts;
            return "Imported successfully!";
        }
    }
}
