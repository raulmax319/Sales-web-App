using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesWebMvc.Migrations
{
    public partial class OtherEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "seller",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true),
                    email = table.Column<string>(type: "longtext", nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    baseSalary = table.Column<double>(type: "double", nullable: false),
                    departmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seller", x => x.id);
                    table.ForeignKey(
                        name: "FK_seller_Department_departmentId",
                        column: x => x.departmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "salesRecord",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    amount = table.Column<double>(type: "double", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    sellerid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salesRecord", x => x.id);
                    table.ForeignKey(
                        name: "FK_salesRecord_seller_sellerid",
                        column: x => x.sellerid,
                        principalTable: "seller",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_salesRecord_sellerid",
                table: "salesRecord",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_seller_departmentId",
                table: "seller",
                column: "departmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "salesRecord");

            migrationBuilder.DropTable(
                name: "seller");
        }
    }
}
