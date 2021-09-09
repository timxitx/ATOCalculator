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
        int taxThresholdId;
        public EmployeeService()
        {
            this.taxThreshold = new TaxThreshold();
            taxThresholdId = 1;
        }
        public ActionResult<IEnumerable<PaySlip>> ExportPaySlip(List<Employee> employees, DataContext context)
        {
            taxThresholds = new int[] { -1, this.taxThreshold.Threshold1, this.taxThreshold.Threshold2, this.taxThreshold.Threshold3, this.taxThreshold.Threshold4, int.MaxValue };
            taxRates = new double[] { this.taxThreshold.TaxRate1, this.taxThreshold.TaxRate2, this.taxThreshold.TaxRate3, this.taxThreshold.TaxRate4, this.taxThreshold.TaxRate5 };

            System.Console.WriteLine("Adding employee...");
            foreach (Employee e in employees)
            {
                System.Console.WriteLine("Add Employee...");
                Employee employeeInDatabase =  (from ee in context.employee where ee.FirstName == e.FirstName && ee.LastName == e.LastName select ee).SingleOrDefault();
                Console.WriteLine(employeeInDatabase);
                var employeeId = e.Id;
                if (employeeInDatabase == null)
                {
                    System.Console.WriteLine("New Record.");
                    context.employee.Add(e);
                } else
                {
                    employeeInDatabase.AnnualSalary = e.AnnualSalary;
                    employeeInDatabase.SuperRate = e.SuperRate;
                    employeeInDatabase.paymentMonth = e.paymentMonth;
                    employeeId = employeeInDatabase.Id;
                }
                context.SaveChanges();

                PaySlip payslipInDatabase = (from ps in context.payslip where ps.EmployeeId == employeeId && ps.TaxThresholdId == taxThresholdId select ps).SingleOrDefault();
                if(payslipInDatabase == null)
                {
                    PaySlip payslip = new PaySlip();
                    payslip.GrossIncome = (int)Math.Round((double)e.AnnualSalary / 12, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = payslip.CalculateIcomeTax(e.AnnualSalary, taxThresholds, taxRates);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                    payslip.Superannuation = (int)Math.Round(payslip.GrossIncome * e.SuperRate, MidpointRounding.AwayFromZero);
                    DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, e.paymentMonth, 1);
                    payslip.FromDate = firstDayOfMonth.Day.ToString();
                    payslip.ToDate = firstDayOfMonth.AddMonths(1).AddDays(-1).Day.ToString();
                    payslip.EmployeeId = employeeId;
                    payslip.TaxThresholdId = taxThresholdId;
                    context.payslip.Add(payslip);
                    context.SaveChanges();
                } else
                {
                    context.payslip.Add(payslipInDatabase);
                }
               
            }
            
            return context.payslip;
        }

        public string AssignTaxThreshold(TaxThreshold tts, DataContext context)
        {
            System.Console.WriteLine("Add TaxThreshold...");
            TaxThreshold taxThresholdInDatabase = (from tt in context.taxThreshold where tt.Threshold1 == tts.Threshold1 && 
                               tt.TaxRate1 == tts.TaxRate1 && tt.Threshold2 == tts.Threshold2 &&
                               tt.TaxRate2 == tts.TaxRate2 && tt.Threshold3 == tts.Threshold3 &&
                               tt.TaxRate3 == tts.TaxRate3 && tt.Threshold4 == tts.Threshold4 &&
                               tt.TaxRate4 == tts.TaxRate4 && tt.TaxRate5 == tts.TaxRate5 select tt).SingleOrDefault();
            if(taxThresholdInDatabase == null)
            {
                context.taxThreshold.Add(tts);
                context.SaveChanges();
                this.taxThreshold = tts;
                taxThresholdId = tts.Id;
                return "Imported successfully!";
            } else
            {
                return "Already had this record!";
            }
        }


        public string clearData(DataContext context)
        {
            context.employee.RemoveRange(context.employee.ToList());
            context.payslip.RemoveRange(context.payslip.ToList());
            TaxThreshold tt = (from t in context.taxThreshold where t.Id == 3 select t).SingleOrDefault();
            context.taxThreshold.Remove(tt);
            context.SaveChanges();
            return "done";
        }
    }
}
