using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class trei : Migration
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

            //migrationBuilder.AlterColumn<string>(
            //    name: "IsApproved",
            //    table: "MEDICI",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    defaultValueSql: "false",
            //    oldClrType: typeof(bool),
            //    oldType: "bit",
            //    oldDefaultValueSql: "(false)");

            migrationBuilder.AddForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE",
                column: "ID_IMAGINE",
                principalTable: "IMAGINI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE");

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsApproved",
            //    table: "MEDICI",
            //    type: "bit",
            //    nullable: false,
            //    defaultValueSql: "(false)",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true,
            //    oldDefaultValueSql: "false");

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
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
