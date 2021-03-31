using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class noua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "PERSOANE",
            //    keyColumn: "ID",
            //    keyValue: new Guid("3659893c-89f2-4a7a-a9ac-f217ebaf5a2e"));

            migrationBuilder.AddColumn<string>(
               name: "MimeType",
               table: "REZULTATE",
               type: "varchar(20)",
               nullable: true);
            //migrationBuilder.RenameColumn(
            //    name: "MymeType",
            //    table: "REZULTATE",
            //    newName: "MimeType");

            //migrationBuilder.InsertData(
            //    table: "PERSOANE",
            //    columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "IsAdmin", "NUME", "PAROLA", "TELEFON", "SEX" },
            //    values: new object[] { new Guid("4e69f5dd-a0f8-4af2-8e81-3a74d4c77cd0"), new DateTime(2021, 3, 4, 12, 24, 57, 403, DateTimeKind.Local).AddTicks(5712), "admin1@admin.com", "Admin", true, "Admin", "admin!", "0712345678", "Feminin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "MimeType",
               table: "REZULTATE");

            //migrationBuilder.DeleteData(
            //    table: "PERSOANE",
            //    keyColumn: "ID",
            //    keyValue: new Guid("4e69f5dd-a0f8-4af2-8e81-3a74d4c77cd0"));

            //migrationBuilder.RenameColumn(
            //    name: "MimeType",
            //    table: "REZULTATE",
            //    newName: "MymeType");

            //migrationBuilder.InsertData(
            //    table: "PERSOANE",
            //    columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "IsAdmin", "NUME", "PAROLA", "TELEFON", "SEX" },
            //    values: new object[] { new Guid("3659893c-89f2-4a7a-a9ac-f217ebaf5a2e"), new DateTime(2021, 3, 4, 12, 18, 20, 435, DateTimeKind.Local).AddTicks(3839), "admin1@admin.com", "Admin", true, "Admin", "admin!", "0712345678", "Feminin" });
        }
    }
}
