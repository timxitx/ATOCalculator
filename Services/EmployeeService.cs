using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATOCalculator.Models;

namespace ATOCalculator.Services
{
    public class EmployeeService
    {
        TaxThreshold taxThreshold;
        List<PaySlip> payslips = new List<PaySlip>();
        int[] taxThresholds;
        double[] taxRates;


        public EmployeeService()
        {
            this.taxThreshold = new TaxThreshold();
        }
        public List<PaySlip> ExportPaySlip(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                PaySlip payslip = new PaySlip();
                Console.WriteLine(this.taxThreshold.TaxRate1);
                taxThresholds = new int[]{ -1, this.taxThreshold.Threshold1, this.taxThreshold.Threshold2, this.taxThreshold.Threshold3, this.taxThreshold.Threshold4, int.MaxValue };
                taxRates = new double[]{this.taxThreshold.TaxRate1, this.taxThreshold.TaxRate2, this.taxThreshold.TaxRate3, this.taxThreshold.TaxRate4, this.taxThreshold.TaxRate5};

                payslip.GrossIncome = (int) Math.Round((double)employee.AnnualSalary / 12, MidpointRounding.AwayFromZero);
                payslip.IncomeTax = payslip.CalculateIcomeTax(employee.AnnualSalary, taxThresholds, taxRates);
                payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                payslip.Superannuation = (int) Math.Round(payslip.GrossIncome * employee.SuperRate, MidpointRounding.AwayFromZero);
                DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, employee.paymentMonth, 1);
                payslip.FromDate = firstDayOfMonth.Day.ToString();
                payslip.ToDate = firstDayOfMonth.AddMonths(1).AddDays(-1).Day.ToString();
                payslips.Add(payslip);
            }
            return payslips;
        }

        public string AssignTaxThreshold(TaxThreshold tts)
        {
            Console.WriteLine(tts.Threshold1);
            this.taxThreshold.Threshold1 = tts.Threshold1;
            this.taxThreshold.TaxRate1 = tts.TaxRate1;
            this.taxThreshold.Threshold2 = tts.Threshold2;
            this.taxThreshold.TaxRate2 = tts.TaxRate2;
            this.taxThreshold.Threshold3 = tts.Threshold3;
            this.taxThreshold.TaxRate3 = tts.TaxRate3;
            this.taxThreshold.Threshold4 = tts.Threshold4;
            this.taxThreshold.TaxRate4 = tts.TaxRate4;
            this.taxThreshold.TaxRate5 = tts.TaxRate5;
            Console.WriteLine(this.taxThreshold.TaxRate1);
            return "Imported successfully!";
        }
    }
}
