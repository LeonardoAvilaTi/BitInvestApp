using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bitinvest.Infra.Migrations
{
    /// <inheritdoc />
    public partial class CampoAprovado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Aprovado",
                table: "MovimentoContaCripto",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aprovado",
                table: "MovimentoContaCripto");
        }
    }
}
