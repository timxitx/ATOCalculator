using Microsoft.EntityFrameworkCore.Migrations;

namespace ATOCalculator.Data.Migrations
{
    public partial class createTaxThresholdTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "TaxThreshold",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
                Threshold1 = table.Column<int>(nullable: false),
                TaxRate1 = table.Column<double>(nullable: false),
                Threshold2 = table.Column<int>(nullable: false),
                TaxRate2 = table.Column<double>(nullable: false),
                Threshold3 = table.Column<int>(nullable: false),
                TaxRate3 = table.Column<double>(nullable: false),
                Threshold4 = table.Column<int>(nullable: false),
                TaxRate4 = table.Column<double>(nullable: false),
                TaxRate5 = table.Column<double>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TaxThreshold", x => x.Id);
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "TaxThreshold");
        }
    }
}
