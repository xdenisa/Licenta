using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class cinci : Migration
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

            migrationBuilder.AddColumn<Guid>(
                name: "IdImage",
                table: "SPECIALIZARI",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SPECIALIZARI_IdImagine",
                table: "SPECIALIZARI",
                column: "IdImage");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_REZULTATE",
            //    table: "REZULTATE",
            //    column: "ID_IMAGINE",
            //    principalTable: "IMAGINI",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SPECIALIZARI_IMAGINI_IdImagine",
                table: "SPECIALIZARI",
                column: "IdImage",
                principalTable: "IMAGINI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_REZULTATE",
                table: "REZULTATE");

            migrationBuilder.DropForeignKey(
                name: "FK_SPECIALIZARI_IMAGINI_IdImagine",
                table: "SPECIALIZARI");

            migrationBuilder.DropIndex(
                name: "IX_SPECIALIZARI_IdImagine",
                table: "SPECIALIZARI");

            migrationBuilder.DropColumn(
                name: "IdImage",
                table: "SPECIALIZARI");

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
