using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraCarros.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoeventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VeiculoEventos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlacaVeiculo = table.Column<string>(type: "varchar(7)", nullable: false),
                    Acao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoEventos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeiculoEventos");
        }
    }
}
