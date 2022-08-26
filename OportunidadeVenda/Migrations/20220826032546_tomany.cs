using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OportunidadeVenda.Migrations
{
    public partial class tomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oportunidade_Usuarios_IdUsuario",
                table: "Oportunidade");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuario",
                table: "Oportunidade",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Oportunidade",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_Oportunidade_Usuarios_IdUsuario",
                table: "Oportunidade",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Oportunidade_Usuarios_IdUsuario",
                table: "Oportunidade");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuario",
                table: "Oportunidade",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cnpj",
                table: "Oportunidade",
                type: "int",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddForeignKey(
                name: "FK_Oportunidade_Usuarios_IdUsuario",
                table: "Oportunidade",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
