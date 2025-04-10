using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProverStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class testfavoritos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoCliente_Clientes_ClienteId",
                table: "EnderecoCliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecoCliente",
                table: "EnderecoCliente");

            migrationBuilder.RenameTable(
                name: "EnderecoCliente",
                newName: "EnderecoClientes");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoCliente_ClienteId",
                table: "EnderecoClientes",
                newName: "IX_EnderecoClientes_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecoClientes",
                table: "EnderecoClientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoClientes_Clientes_ClienteId",
                table: "EnderecoClientes",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoClientes_Clientes_ClienteId",
                table: "EnderecoClientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecoClientes",
                table: "EnderecoClientes");

            migrationBuilder.RenameTable(
                name: "EnderecoClientes",
                newName: "EnderecoCliente");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoClientes_ClienteId",
                table: "EnderecoCliente",
                newName: "IX_EnderecoCliente_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecoCliente",
                table: "EnderecoCliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoCliente_Clientes_ClienteId",
                table: "EnderecoCliente",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
