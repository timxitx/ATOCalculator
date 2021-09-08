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
        DatabaseService databaseService;
        List<PaySlip> payslips = new List<PaySlip>();
        int[] taxThresholds;
        double[] taxRates;
        int taxThresholdId = 1;
        string query;


        public EmployeeService()
        {
            this.taxThreshold = new TaxThreshold();
            this.databaseService = new DatabaseService();
        }
        public List<PaySlip> ExportPaySlip(List<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                query = "INSERT INTO Employee(FirstName, LastName, AnnualSalary, SuperRate, PaymentMonth) " +
                    "VALUES('" + employee.FirstName + "', '" + employee.LastName + "', " + employee.AnnualSalary + ", " + employee.SuperRate + ", " + employee.paymentMonth + "); SELECT SCOPE_IDENTITY();";
                int employeeId = databaseService.InsertData(query);

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
                payslip.EmployeeId = employeeId;
                payslip.TaxThresholdId = taxThresholdId;
                payslips.Add(payslip);

                query = "INSERT INTO PaySlip(GrossIncome, IncomeTax, NetIncome, Superannuation, FromDate, ToDate, EmployeeId, TaxThresholdId) " +
                    "VALUES(" + payslip.GrossIncome + ", " + payslip.IncomeTax + ", " + payslip.NetIncome + ", " + payslip.Superannuation + ", " + 
                    payslip.FromDate + ", " + payslip.ToDate + ", " + payslip.EmployeeId + ", " + payslip.TaxThresholdId + ");";
                databaseService.InsertData(query);
            }
            return payslips;
        }

        public string AssignTaxThreshold(TaxThreshold tts)
        {
            this.taxThreshold.Threshold1 = tts.Threshold1;
            this.taxThreshold.TaxRate1 = tts.TaxRate1;
            this.taxThreshold.Threshold2 = tts.Threshold2;
            this.taxThreshold.TaxRate2 = tts.TaxRate2;
            this.taxThreshold.Threshold3 = tts.Threshold3;
            this.taxThreshold.TaxRate3 = tts.TaxRate3;
            this.taxThreshold.Threshold4 = tts.Threshold4;
            this.taxThreshold.TaxRate4 = tts.TaxRate4;
            this.taxThreshold.TaxRate5 = tts.TaxRate5;

            query = "INSERT INTO TaxThreshold(Threshold1, TaxRate1, Threshold2, TaxRate2, Threshold3, TaxRate3, Threshold4, TaxRate4, TaxRate5)" +
                "VALUES(" +taxThreshold.Threshold1 + ", " + taxThreshold.TaxRate1 + ", " + taxThreshold.Threshold2 + ", " + taxThreshold.TaxRate2 + 
                ", " + taxThreshold.Threshold3 + ", " + taxThreshold.TaxRate3 + ", " + taxThreshold.Threshold4 + ", " + taxThreshold.TaxRate4 + ", " + taxThreshold.TaxRate5 + "); SELECT SCOPE_IDENTITY();";
            this.taxThresholdId = databaseService.InsertData(query);
            return "Imported successfully!";
        }
    }
}
