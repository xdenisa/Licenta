using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class zece : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "PERSOANE",
            //    keyColumn: "ID",
            //    keyValue: new Guid("4e69f5dd-a0f8-4af2-8e81-3a74d4c77cd0"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID_MEDIC",
                table: "PORTOFOLIU",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            //migrationBuilder.InsertData(
            //    table: "PERSOANE",
            //    columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "IsAdmin", "NUME", "PAROLA", "TELEFON", "SEX" },
            //    values: new object[] { new Guid("a76eca68-cb88-4142-bb12-985be99e3e4c"), new DateTime(2021, 3, 4, 14, 5, 26, 155, DateTimeKind.Local).AddTicks(1460), "admin1@admin.com", "Admin", true, "Admin", "admin!", "0712345678", "Feminin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "PERSOANE",
            //    keyColumn: "ID",
            //    keyValue: new Guid("a76eca68-cb88-4142-bb12-985be99e3e4c"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID_MEDIC",
                table: "PORTOFOLIU",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

          //migrationBuilder.InsertData(
          //      table: "PERSOANE",
          //      columns: new[] { "ID", "DATA_NASTERII", "EMAIL", "PRENUME", "IsAdmin", "NUME", "PAROLA", "TELEFON", "SEX" },
          //      values: new object[] { new Guid("4e69f5dd-a0f8-4af2-8e81-3a74d4c77cd0"), new DateTime(2021, 3, 4, 12, 24, 57, 403, DateTimeKind.Local).AddTicks(5712), "admin1@admin.com", "Admin", true, "Admin", "admin!", "0712345678", "Feminin" });
        }
    }
}
