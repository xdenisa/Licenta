using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class opt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE");

            //migrationBuilder.DropUniqueConstraint(
            //    name: "AK_IMAGINI_TempId",
            //    table: "IMAGINI");

            migrationBuilder.DeleteData(
                table: "PERSOANE",
                keyColumn: "ID",
                keyValue: new Guid("34076325-4D5F-4FD9-B18B-56ACEF9D7881"));

            //migrationBuilder.DropColumn(
            //    name: "TempId",
            //    table: "IMAGINI");

            migrationBuilder.InsertData(
                table: "PERSOANE",
                columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "IsAdmin", "NUME", "PAROLA", "TELEFON", "SEX" },
                values: new object[] { new Guid("d2397090-94f3-4741-9cb4-3557973db478"), new DateTime(2021, 2, 26, 12, 47, 38, 570, DateTimeKind.Local).AddTicks(1080), "admin1@admin.com", "Admin", true, "Admin", "admin!", "0712345678", "Feminin" });

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
                keyValue: new Guid("d2397090-94f3-4741-9cb4-3557973db478"));

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

            //migrationBuilder.InsertData(
            //    table: "PERSOANE",
            //    columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "NUME", "PAROLA", "TELEFON", "SEX" },
            //    values: new object[] { new Guid("823af81f-e7e9-4f74-b119-86313910c22c"), new DateTime(2021, 2, 26, 12, 46, 10, 832, DateTimeKind.Local).AddTicks(3762), "admin1@admin.com", "Admin", "Admin", "admin!", "0712345678", "Feminin" });

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
