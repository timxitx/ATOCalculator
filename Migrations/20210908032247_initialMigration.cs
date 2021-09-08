using Microsoft.EntityFrameworkCore.Migrations;

namespace ATOCalculator.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualSalary = table.Column<int>(type: "int", nullable: false),
                    SuperRate = table.Column<double>(type: "float", nullable: false),
                    paymentMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payslip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossIncome = table.Column<int>(type: "int", nullable: false),
                    IncomeTax = table.Column<int>(type: "int", nullable: false),
                    NetIncome = table.Column<int>(type: "int", nullable: false),
                    Superannuation = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TaxThresholdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payslip", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "taxThreshold",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Threshold1 = table.Column<int>(type: "int", nullable: false),
                    TaxRate1 = table.Column<double>(type: "float", nullable: false),
                    Threshold2 = table.Column<int>(type: "int", nullable: false),
                    TaxRate2 = table.Column<double>(type: "float", nullable: false),
                    Threshold3 = table.Column<int>(type: "int", nullable: false),
                    TaxRate3 = table.Column<double>(type: "float", nullable: false),
                    Threshold4 = table.Column<int>(type: "int", nullable: false),
                    TaxRate4 = table.Column<double>(type: "float", nullable: false),
                    TaxRate5 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taxThreshold", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "payslip");

            migrationBuilder.DropTable(
                name: "taxThreshold");
        }
    }
}
