using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProverStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class clienteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoClienteId",
                table: "Clientes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Clientes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Clientes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Clientes",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EnderecoClienteId",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
