using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class doi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE");

            //migrationBuilder.DropUniqueConstraint(
            //    name: "AK_IMAGINI_TempId",
            //    table: "IMAGINI");

            //migrationBuilder.DropColumn(
            //    name: "TempId",
            //    table: "IMAGINI");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "ID_IMAGINE",
            //    table: "REZULTATE",
            //    type: "varbinary(16)",
            //    nullable: false,
            //    defaultValue: new byte[0],
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(16)",
            //    oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsApproved",
                table: "MEDICI",
                type: "nvarchar(10)",
                nullable: true,
                defaultValueSql: "('false')");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE",
            //    column: "ID_IMAGINE",
            //    principalTable: "IMAGINI",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "MEDICI");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ID_IMAGINE",
                table: "REZULTATE",
                type: "varbinary(16)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(16)");

            migrationBuilder.AddColumn<byte[]>(
                name: "TempId",
                table: "IMAGINI",
                type: "varbinary(900)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_IMAGINI_TempId",
                table: "IMAGINI",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE",
                column: "ID_IMAGINE",
                principalTable: "IMAGINI",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
