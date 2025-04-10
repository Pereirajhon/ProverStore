using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProverStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class testfavorito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favoritos",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "Favorito",
                table: "Produtos",
                newName: "Favoritos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favoritos",
                table: "Favoritos",
                columns: new[] { "ClienteId", "ProdutoId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Favoritos",
                table: "Favoritos");

            migrationBuilder.RenameColumn(
                name: "Favoritos",
                table: "Produtos",
                newName: "Favorito");

            migrationBuilder.AddColumn<Guid>(
                name: "ProdutoId",
                table: "Produtos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favoritos",
                table: "Favoritos",
                column: "Id");
        }
    }
}
