using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SovosWebApi.Repository.Migrations
{
    public partial class firstVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceHeader",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SenderTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ReceiverTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceHeader", x => x.InvoiceId);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "Decimal", nullable: false),
                    InvoiceId = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLine_InvoiceHeader_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "InvoiceHeader",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLine_InvoiceId",
                table: "InvoiceLine",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceLine");

            migrationBuilder.DropTable(
                name: "InvoiceHeader");
        }
    }
}
