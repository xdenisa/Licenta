using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.DataAccess.Migrations
{
    public partial class unu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IMAGINI",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IMAGINE = table.Column<byte[]>(type: "image", nullable: false),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormatImagine = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMAGINI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEDICAMENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    DENUMIRE_MEDICAMENT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MOD_ADMINISTRARE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICAMENTE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PERSOANE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    NUME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRENUME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAROLA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DATA_NASTERII = table.Column<DateTime>(type: "date", nullable: true),
                    SEX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TELEFON = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSOANE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SPECIALIZARI",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    DENUMIRE_SPECIALIZARE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPECIALIZARI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "REZULTATE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    OBSERVATII = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DOCUMENT = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ID_IMAGINE = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DATA_EMITERII = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REZULTATE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REZULTATE",
                        column: x => x.ID_IMAGINE,
                        principalTable: "IMAGINI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTI",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ID_PERSOANA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ADRESA = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ID_IMAGINE = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PACIENTI",
                        column: x => x.ID_IMAGINE,
                        principalTable: "IMAGINI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PACIENTI_PERSOANE",
                        column: x => x.ID_PERSOANA,
                        principalTable: "PERSOANE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MEDICI",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ID_PERSOANA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COD_IDENTIFICARE = table.Column<int>(type: "int", maxLength: 10, nullable: true),
                    COLEGIUL_MEDICILOR = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    ID_SPECIALIZARE = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SPITAL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    COMPETENTE = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    STUDII = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DESCRIERE = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ID_IMAGINE = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MEDICI_PERSOANE",
                        column: x => x.ID_PERSOANA,
                        principalTable: "PERSOANE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MEDICI1",
                        column: x => x.ID_IMAGINE,
                        principalTable: "IMAGINI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MEDICI2",
                        column: x => x.ID_SPECIALIZARE,
                        principalTable: "SPECIALIZARI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TRATAMENTE",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ID_PACIENT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_MEDICAMENT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SCHEMA_TRATAMENT = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    OBSERVATII = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRATAMENTE", x => x.ID);
                    table.ForeignKey(
                        name: "FK__TRATAMENTE_MEDICAMENT",
                        column: x => x.ID_MEDICAMENT,
                        principalTable: "MEDICAMENTE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TRATAMENTE_PACIENT",
                        column: x => x.ID_PACIENT,
                        principalTable: "PACIENTI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PORTOFOLIU",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ID_PACIENT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_MEDIC = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_REZULTAT = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PORTOFOLIU", x => x.ID);
                    table.ForeignKey(
                        name: "FK__PORTOFOLIU_MEDIC",
                        column: x => x.ID_MEDIC,
                        principalTable: "MEDICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__PORTOFOLIU_REZULTAT",
                        column: x => x.ID_REZULTAT,
                        principalTable: "REZULTATE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PORTOFOLIU_PACIENT",
                        column: x => x.ID_PACIENT,
                        principalTable: "PACIENTI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PROGRAMARI",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ID_PACIENT = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_MEDIC = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATA_PROGRAMARII = table.Column<DateTime>(type: "datetime", nullable: false),
                    DETALII = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROGRAMARI", x => x.ID);
                    table.ForeignKey(
                        name: "FK__PROGRAMARI_MEDIC",
                        column: x => x.ID_MEDIC,
                        principalTable: "MEDICI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROGRAMARI_PACIENT",
                        column: x => x.ID_PACIENT,
                        principalTable: "PACIENTI",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MEDICI_ID_IMAGINE",
                table: "MEDICI",
                column: "ID_IMAGINE");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICI_ID_PERSOANA",
                table: "MEDICI",
                column: "ID_PERSOANA");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICI_ID_SPECIALIZARE",
                table: "MEDICI",
                column: "ID_SPECIALIZARE");

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTI_ID_IMAGINE",
                table: "PACIENTI",
                column: "ID_IMAGINE");

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTI_ID_PERSOANA",
                table: "PACIENTI",
                column: "ID_PERSOANA");

            migrationBuilder.CreateIndex(
                name: "IX_PORTOFOLIU_ID_MEDIC",
                table: "PORTOFOLIU",
                column: "ID_MEDIC");

            migrationBuilder.CreateIndex(
                name: "IX_PORTOFOLIU_ID_PACIENT",
                table: "PORTOFOLIU",
                column: "ID_PACIENT");

            migrationBuilder.CreateIndex(
                name: "IX_PORTOFOLIU_ID_REZULTAT",
                table: "PORTOFOLIU",
                column: "ID_REZULTAT");

            migrationBuilder.CreateIndex(
                name: "IX_PROGRAMARI_ID_MEDIC",
                table: "PROGRAMARI",
                column: "ID_MEDIC");

            migrationBuilder.CreateIndex(
                name: "IX_PROGRAMARI_ID_PACIENT",
                table: "PROGRAMARI",
                column: "ID_PACIENT");

            migrationBuilder.CreateIndex(
                name: "IX_REZULTATE_ID_IMAGINE",
                table: "REZULTATE",
                column: "ID_IMAGINE");

            migrationBuilder.CreateIndex(
                name: "IX_TRATAMENTE_ID_MEDICAMENT",
                table: "TRATAMENTE",
                column: "ID_MEDICAMENT");

            migrationBuilder.CreateIndex(
                name: "IX_TRATAMENTE_ID_PACIENT",
                table: "TRATAMENTE",
                column: "ID_PACIENT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PORTOFOLIU");

            migrationBuilder.DropTable(
                name: "PROGRAMARI");

            migrationBuilder.DropTable(
                name: "TRATAMENTE");

            migrationBuilder.DropTable(
                name: "REZULTATE");

            migrationBuilder.DropTable(
                name: "MEDICI");

            migrationBuilder.DropTable(
                name: "MEDICAMENTE");

            migrationBuilder.DropTable(
                name: "PACIENTI");

            migrationBuilder.DropTable(
                name: "SPECIALIZARI");

            migrationBuilder.DropTable(
                name: "IMAGINI");

            migrationBuilder.DropTable(
                name: "PERSOANE");
        }
    }
}
