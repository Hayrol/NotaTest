using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotaTest.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NOTA",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Cuerpo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NOTA__4B2ACFF2CA99158B", x => x.IdNota);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOTA");
        }
    }
}
