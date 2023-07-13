using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class includeandassociationfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentImage",
                table: "Subscribe");

            migrationBuilder.DropColumn(
                name: "AttachmentVideo",
                table: "Subscribe");

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attachment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifieddDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscribeFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscribeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscribeFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscribeFile_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscribeFile_Subscribe_SubscribeId",
                        column: x => x.SubscribeId,
                        principalTable: "Subscribe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscribeFile_FileId",
                table: "SubscribeFile",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribeFile_SubscribeId",
                table: "SubscribeFile",
                column: "SubscribeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscribeFile");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentImage",
                table: "Subscribe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttachmentVideo",
                table: "Subscribe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
