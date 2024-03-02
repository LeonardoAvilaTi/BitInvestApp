using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitinvest.Infra.Migrations
{
    /// <inheritdoc />
    public partial class ContaReais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimentoContaReais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescricaoOperacao = table.Column<string>(type: "varchar(250)", nullable: false),
                    TipoOperacao = table.Column<short>(type: "smallint", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AlteradoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AlteradoPorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoContaReais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoContaReais_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoContaReais_ClienteId",
                table: "MovimentoContaReais",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentoContaReais");
        }
    }
}
