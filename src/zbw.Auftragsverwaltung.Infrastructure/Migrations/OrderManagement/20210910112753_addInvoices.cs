using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zbw.Auftragsverwaltung.Infrastructure.Migrations.OrderManagement
{
    public partial class addInvoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Netto = table.Column<double>(type: "float", nullable: false),
                    Brutto = table.Column<double>(type: "float", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AddressId",
                table: "Invoices",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
