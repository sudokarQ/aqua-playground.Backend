using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AquaPlayground.Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class orderChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_services_ServiceId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ServiceId",
                table: "orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2aba2927-3e55-4606-8ef8-463f1998a9a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a373e9ae-0b6d-4d8a-badf-5d5746f2ad25");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "orders");

            migrationBuilder.CreateTable(
                name: "OrderService",
                columns: table => new
                {
                    OrdersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServicesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderService", x => new { x.OrdersId, x.ServicesId });
                    table.ForeignKey(
                        name: "FK_OrderService_orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderService_services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "04d3a9e8-88b0-41b2-8e94-00abf8b16cf6", null, "Admin", "ADMIN" },
                    { "16f9a467-0871-4b91-bd68-7d4757c874a8", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderService_ServicesId",
                table: "OrderService",
                column: "ServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderService");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04d3a9e8-88b0-41b2-8e94-00abf8b16cf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16f9a467-0871-4b91-bd68-7d4757c874a8");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ServicePrice",
                table: "orders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2aba2927-3e55-4606-8ef8-463f1998a9a8", null, "Admin", "ADMIN" },
                    { "a373e9ae-0b6d-4d8a-badf-5d5746f2ad25", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_ServiceId",
                table: "orders",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_services_ServiceId",
                table: "orders",
                column: "ServiceId",
                principalTable: "services",
                principalColumn: "Id");
        }
    }
}
