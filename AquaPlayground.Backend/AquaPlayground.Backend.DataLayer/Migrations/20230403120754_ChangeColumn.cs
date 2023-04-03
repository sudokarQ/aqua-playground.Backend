using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaPlayground.Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId1",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_UserId1",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "TotaPrice",
                table: "orders",
                newName: "TotalPrice");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "orders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_UserId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_UserId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "orders",
                newName: "TotaPrice");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "orders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId1",
                table: "orders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_UserId1",
                table: "orders",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
