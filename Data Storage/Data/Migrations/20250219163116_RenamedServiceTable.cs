using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Services_ServiceId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Services",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Projects",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ServiceId",
                table: "Projects",
                newName: "IX_Projects_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Services_ProductId",
                table: "Projects",
                column: "ProductId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Services_ProductId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Services",
                newName: "ServiceName");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Projects",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_ProductId",
                table: "Projects",
                newName: "IX_Projects_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Services_ServiceId",
                table: "Projects",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
