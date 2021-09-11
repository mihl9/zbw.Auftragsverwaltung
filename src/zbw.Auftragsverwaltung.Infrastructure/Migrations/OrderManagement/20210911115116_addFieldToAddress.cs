using Microsoft.EntityFrameworkCore.Migrations;

namespace zbw.Auftragsverwaltung.Infrastructure.Migrations.OrderManagement
{
    public partial class addFieldToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recipient",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recipient",
                table: "Addresses");
        }
    }
}
