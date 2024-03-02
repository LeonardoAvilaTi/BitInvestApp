using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitinvest.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoContaReais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoContaReais_Clientes_ClienteId",
                table: "MovimentoContaReais");

            migrationBuilder.DropIndex(
                name: "IX_MovimentoContaReais_ClienteId",
                table: "MovimentoContaReais");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "MovimentoContaReais",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ClienteId",
                table: "MovimentoContaReais",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoContaReais_ClienteId",
                table: "MovimentoContaReais",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoContaReais_Clientes_ClienteId",
                table: "MovimentoContaReais",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
