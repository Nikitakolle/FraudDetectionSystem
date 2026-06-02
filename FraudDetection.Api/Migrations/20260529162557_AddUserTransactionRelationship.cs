using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FraudDetection.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTransactionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TransactionHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistories_UserId",
                table: "TransactionHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionHistories_Users_UserId",
                table: "TransactionHistories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionHistories_Users_UserId",
                table: "TransactionHistories");

            migrationBuilder.DropIndex(
                name: "IX_TransactionHistories_UserId",
                table: "TransactionHistories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransactionHistories");
        }
    }
}
