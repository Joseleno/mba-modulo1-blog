using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbaBlog.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Posts",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(30)");

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Posts",
                type: "VARCHAR(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1024)");

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "Comentarios",
                type: "VARCHAR(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(512)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Posts",
                type: "VARCHAR(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "Texto",
                table: "Posts",
                type: "VARCHAR(1024)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(1500)",
                oldMaxLength: 1500);

            migrationBuilder.AlterColumn<string>(
                name: "Comentario",
                table: "Comentarios",
                type: "VARCHAR(512)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(500)",
                oldMaxLength: 500);
        }
    }
}
