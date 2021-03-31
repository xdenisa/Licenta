using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class patru : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "PERSOANE",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PERSOANE_EMAIL",
                table: "PERSOANE",
                column: "EMAIL",
                unique: true);

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
            //migrationBuilder.DropForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE");

            migrationBuilder.DropIndex(
                name: "IX_PERSOANE_EMAIL",
                table: "PERSOANE");

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "PERSOANE",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
