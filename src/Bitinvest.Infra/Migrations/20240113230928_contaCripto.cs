using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitinvest.Infra.Migrations
{
    /// <inheritdoc />
    public partial class contaCripto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentoContaCripto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriptoMoeda = table.Column<string>(type: "varchar(30)", nullable: false),
                    DescricaoOperacao = table.Column<string>(type: "varchar(250)", nullable: false),
                    TipoOperacao = table.Column<short>(type: "smallint", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoContaCripto", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentoContaCripto");
        }
    }
}
