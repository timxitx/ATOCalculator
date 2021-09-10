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
            this.taxThreshold.Id = 1;
        }
        public List<Result> ExportPaySlip(List<Employee> employees, DataContext context)
        {
            taxThresholds = new int[] { -1, this.taxThreshold.Threshold1, this.taxThreshold.Threshold2, this.taxThreshold.Threshold3, this.taxThreshold.Threshold4, int.MaxValue };
            taxRates = new double[] { this.taxThreshold.TaxRate1, this.taxThreshold.TaxRate2, this.taxThreshold.TaxRate3, this.taxThreshold.TaxRate4, this.taxThreshold.TaxRate5 };
            List<Result> results = new List<Result>();

            System.Console.WriteLine("Adding employee...");
            foreach (Employee e in employees)
            {
                Result result = new Result();
                System.Console.WriteLine("Add Employee...");
                Employee employeeInDatabase =  (from ee in context.employee where ee.FirstName == e.FirstName && ee.LastName == e.LastName select ee).SingleOrDefault();
                Console.WriteLine(employeeInDatabase);
                var employeeId = 1;
                if (employeeInDatabase == null)
                {
                    System.Console.WriteLine("New Record.");
                    context.employee.Add(e);
                    employeeId = e.Id;
                    result.employee = e;
                } else
                {
                    employeeInDatabase.AnnualSalary = e.AnnualSalary;
                    employeeInDatabase.SuperRate = e.SuperRate;
                    employeeInDatabase.paymentMonth = e.paymentMonth;
                    employeeId = employeeInDatabase.Id;
                    result.employee = employeeInDatabase;
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.WriteLine("Failed to import employee:" + e.FirstName + e.LastName);
                    Console.WriteLine(error);
                }



                PaySlip payslipInDatabase = (from ps in context.payslip where ps.EmployeeId == employeeId && ps.TaxThresholdId == this.taxThreshold.Id select ps).SingleOrDefault();
                PaySlip payslip = new PaySlip();
                if (payslipInDatabase == null)
                {
                    payslip.GrossIncome = payslip.SetGrossIncome(e.AnnualSalary);
                    payslip.IncomeTax = payslip.SetIcomeTax(e.AnnualSalary, taxThresholds, taxRates);
                    payslip.NetIncome = payslip.SetNetIncome();
                    payslip.Superannuation = payslip.SetSuperannuation(e.SuperRate);
                    payslip.FromDate = payslip.SetFromDate(e.paymentMonth);
                    payslip.ToDate = payslip.SetToDate(e.paymentMonth);
                    payslip.EmployeeId = employeeId;
                    payslip.TaxThresholdId = this.taxThreshold.Id;
                    context.payslip.Add(payslip);
                    context.SaveChanges();
                    result.payslip = payslip;
                } else
                {
                    payslipInDatabase.GrossIncome = payslipInDatabase.SetGrossIncome(e.AnnualSalary);
                    payslipInDatabase.IncomeTax = payslipInDatabase.SetIcomeTax(e.AnnualSalary, taxThresholds, taxRates);
                    payslipInDatabase.NetIncome = payslipInDatabase.SetNetIncome();
                    payslipInDatabase.Superannuation = payslipInDatabase.SetSuperannuation(e.SuperRate);
                    payslipInDatabase.FromDate = payslipInDatabase.SetFromDate(e.paymentMonth);
                    payslipInDatabase.ToDate = payslipInDatabase.SetToDate(e.paymentMonth);
                    result.payslip = payslipInDatabase;
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception error)
                {
                    Console.WriteLine("Failed to import payslip");
                    Console.WriteLine(error);
                }
                results.Add(result);
            }
            
            return results;
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
                try
                {
                    context.taxThreshold.Add(tts);
                    context.SaveChanges();
                    this.taxThreshold = tts;
                    return "Imported successfully!";
                } catch (Exception error)
                {
                    Console.WriteLine(error);
                    return "Failed to import!";
                }
            } else
            {
                this.taxThreshold = taxThresholdInDatabase;
                return "Already had this record!";
            }
        }
    }
}
