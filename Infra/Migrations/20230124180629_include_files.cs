using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class includefiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Subscribe");

            migrationBuilder.AddColumn<string>(
                name: "AttachmenteImage",
                table: "Subscribe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttachmenteVideo",
                table: "Subscribe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmenteImage",
                table: "Subscribe");

            migrationBuilder.DropColumn(
                name: "AttachmenteVideo",
                table: "Subscribe");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Subscribe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
