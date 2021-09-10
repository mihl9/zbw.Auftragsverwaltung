using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace zbw.Auftragsverwaltung.Infrastructure.Migrations.OrderManagement
{
    public partial class UpdateAddressToHisotrizeRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Addresses_AddressId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AddressId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddressValidFrom",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "Addresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "Addresses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                columns: new[] { "Id", "ValidFrom" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AddressId_AddressValidFrom",
                table: "Invoices",
                columns: new[] { "AddressId", "AddressValidFrom" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Addresses_AddressId_AddressValidFrom",
                table: "Invoices",
                columns: new[] { "AddressId", "AddressValidFrom" },
                principalTable: "Addresses",
                principalColumns: new[] { "Id", "ValidFrom" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Addresses_AddressId_AddressValidFrom",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AddressId_AddressValidFrom",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressValidFrom",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AddressId",
                table: "Invoices",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Addresses_AddressId",
                table: "Invoices",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
