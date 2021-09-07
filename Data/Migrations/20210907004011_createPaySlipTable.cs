using Microsoft.EntityFrameworkCore.Migrations;

namespace ATOCalculator.Data.Migrations
{
    public partial class createPaySlipTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "PaySlip",
               columns: table => new
               {
                   Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
                   GrossIncome = table.Column<int>(nullable: false),
                   IncomeTax = table.Column<int>(nullable: false),
                   NetIcome = table.Column<int>(nullable: false),
                   superannuation = table.Column<int>(nullable: false),
                   FromDate = table.Column<string>(nullable: false),
                   ToDate = table.Column<string>(nullable: false),
                   EmployeeId = table.Column<int>(nullable: false),
                   TaxThresholdId = table.Column<int>(nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Payslip", x => x.Id);
               });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaySlip");
        }
    }
}
