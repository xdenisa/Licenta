using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class sapte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE");

            //migrationBuilder.DropUniqueConstraint(
            //    name: "AK_IMAGINI_TempId",
            //    table: "IMAGINI");

            //migrationBuilder.DropColumn(
            //    name: "TempId",
            //    table: "IMAGINI");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "PERSOANE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "PERSOANE",
                columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "NUME", "PAROLA", "TELEFON", "SEX" },
                values: new object[] { new Guid("34076325-4d5f-4fd9-b18b-56acef9d7881"), new DateTime(2021, 2, 26, 12, 44, 3, 758, DateTimeKind.Local).AddTicks(1914), "admin1@admin.com", "Admin", "Admin", "admin!", "0712345678", "Feminin" });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE",
            //    column: "ID_IMAGINE",
            //    principalTable: "IMAGINI",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE");

            migrationBuilder.DeleteData(
                table: "PERSOANE",
                keyColumn: "ID",
                keyValue: new Guid("34076325-4d5f-4fd9-b18b-56acef9d7881"));

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "PERSOANE");

            //migrationBuilder.AddColumn<byte[]>(
            //    name: "TempId",
            //    table: "IMAGINI",
            //    type: "varbinary(900)",
            //    nullable: false,
            //    defaultValue: new byte[0]);

            //migrationBuilder.AddUniqueConstraint(
            //    name: "AK_IMAGINI_TempId",
            //    table: "IMAGINI",
            //    column: "TempId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE",
            //    column: "ID_IMAGINE",
            //    principalTable: "IMAGINI",
            //    principalColumn: "TempId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
