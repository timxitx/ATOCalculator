using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATOCalculator.Models
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> employee { get; set; }

        public DbSet<TaxThreshold> taxThreshold { get; set; }

        public DbSet<PaySlip> payslip { get; set; }
    }
}
