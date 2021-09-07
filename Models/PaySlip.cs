using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATOCalculator.Models
{
    public class PaySlip
    {
        [Key]
        public int Id { get; set; }
        public int GrossIncome { get; set; }
        public int IncomeTax { get; set; }
        public int NetIncome { get; set; }
        public int Superannuation { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int EmployeeId { get; set; }
        public int TaxThresholdId { get; set; }

        public int CalculateIcomeTax(int salary, int[] taxthresholds, double[] taxRates)
        {
            int taxBase = 0;
            double incomeTax = 0;
            for(int i=0; i<taxthresholds.Length-1; i++)
            {
                if (salary >= taxthresholds[i]+1 && salary < taxthresholds[i+1])
                {
                    incomeTax = (taxBase + (salary - taxthresholds[i] + 1) * taxRates[i]) / 12;
                    break;
                }
                taxBase += (int) Math.Round((taxthresholds[i + 1] - taxthresholds[i] + 1) * taxRates[i], MidpointRounding.AwayFromZero);
                Console.WriteLine("taxbase"+i+":" + taxBase);
            }
            return (int) Math.Round(incomeTax, MidpointRounding.AwayFromZero);
        }
    }
}
