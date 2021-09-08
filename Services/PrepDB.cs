using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ATOCalculator.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ATOCalculator.Services
{
    public class PrepDB
    {
        public static void Prepopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>());
            }
        }

        public static void SeedData(DataContext context)
        {
            System.Console.WriteLine("appling migrations...");
            context.Database.Migrate();
            if(!context.taxThreshold.Any())
            {
                System.Console.WriteLine("Adding data...");
                context.taxThreshold.Add(
                    new TaxThreshold()
                    {
                        Threshold1 = 18200,
                        TaxRate1 = 0,
                        Threshold2 = 37000,
                        TaxRate2 = 0.19,
                        Threshold3 = 87000,
                        TaxRate3 = 0.325,
                        Threshold4 = 180000,
                        TaxRate4 = 0.37,
                        TaxRate5 = 0.45
                    });
                context.SaveChanges();
            } else
            {
                System.Console.WriteLine("Already have data");
            }
        }
    }
}
