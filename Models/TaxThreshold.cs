using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ATOCalculator.Models
{
    public class TaxThreshold
    {
        [Key]
        public int Id { get; set; }
        public int Threshold1 { get; set; } = 18200;
        public double TaxRate1 { get; set; } = 0;
        public int Threshold2 { get; set; } = 37000;
        public double TaxRate2 { get; set; } = 0.19;
        public int Threshold3 { get; set; } = 87000;
        public double TaxRate3 { get; set; } = 0.325;
        public int Threshold4 { get; set; } = 180000;
        public double TaxRate4 { get; set; } = 0.37;
        public double TaxRate5 { get; set; } = 0.45;

        public TaxThreshold()
        {

        }
    }
}
