using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinCashly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StrucutureUserMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Goals_UserId",
                table: "Goals");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Goals",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Accounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Goals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId",
                table: "Accounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Users_UserId",
                table: "Goals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
