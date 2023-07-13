using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class modifynamesattrattachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmenteVideo",
                table: "Subscribe",
                newName: "AttachmentVideo");

            migrationBuilder.RenameColumn(
                name: "AttachmenteImage",
                table: "Subscribe",
                newName: "AttachmentImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AttachmentVideo",
                table: "Subscribe",
                newName: "AttachmenteVideo");

            migrationBuilder.RenameColumn(
                name: "AttachmentImage",
                table: "Subscribe",
                newName: "AttachmenteImage");
        }
    }
}
