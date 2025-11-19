using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinCashly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameUpdatedAtColumnMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Transactions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Goals",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Categories",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Accounts",
                newName: "UpdatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Transactions",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Goals",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Categories",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Accounts",
                newName: "UpdateAt");
        }
    }
}
