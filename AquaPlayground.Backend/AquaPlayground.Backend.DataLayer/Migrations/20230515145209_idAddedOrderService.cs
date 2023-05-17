using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AquaPlayground.Backend.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class idAddedOrderService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_order_services",
                table: "order_services");

            

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "order_services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_services",
                table: "order_services",
                column: "Id");

            

            migrationBuilder.CreateIndex(
                name: "IX_order_services_OrderId",
                table: "order_services",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_order_services",
                table: "order_services");

            migrationBuilder.DropIndex(
                name: "IX_order_services_OrderId",
                table: "order_services");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "015efa13-716d-4212-aeac-2617f61066f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f7cb7bc-ff71-4740-a2d9-acdc86010658");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "order_services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_order_services",
                table: "order_services",
                columns: new[] { "OrderId", "ServiceId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "95ec954c-d6ea-4f10-8dd9-0aed19147131", null, "Admin", "ADMIN" },
                    { "f53d6395-fa36-4145-8a2f-1885011f10a0", null, "User", "USER" }
                });
        }
    }
}
