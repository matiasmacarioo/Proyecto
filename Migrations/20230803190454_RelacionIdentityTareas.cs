using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto.Migrations
{
    public partial class RelacionIdentityTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Tarea");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioID",
                table: "Tarea",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_UsuarioID",
                table: "Tarea",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarea_AspNetUsers_UsuarioID",
                table: "Tarea",
                column: "UsuarioID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarea_AspNetUsers_UsuarioID",
                table: "Tarea");

            migrationBuilder.DropIndex(
                name: "IX_Tarea_UsuarioID",
                table: "Tarea");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Tarea");

            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Tarea",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
