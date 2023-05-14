using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AquaPlayground.Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class deleteJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_services");

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
                keyValue: "14a1b23a-ea45-4525-80da-57b25fce5494");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db2adf47-35bf-464a-9996-9798c550f70e");

            migrationBuilder.CreateTable(
                name: "order_services",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_services", x => new { x.OrderId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_order_services_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_services_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88518f85-c584-4c10-a9d0-9b28e05c8dd2", null, "User", "USER" },
                    { "c789d51b-dbe9-477b-a9f6-d9517e9912bc", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_services_ServiceId",
                table: "order_services",
                column: "ServiceId");
        }
    }
}
