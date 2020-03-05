using Microsoft.EntityFrameworkCore.Migrations;

namespace Postinger.Migrations
{
    public partial class AddComentarioAUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Comment",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_Usuario",
                table: "Comment",
                column: "Usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_Usuario",
                table: "Comment",
                column: "Usuario",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_Usuario",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_Usuario",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
