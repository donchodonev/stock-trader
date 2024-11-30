using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrader.Infrastructure.Data.Migrations.OrderService
{
    /// <inheritdoc />
    public partial class AddOrderStatusReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusReason",
                table: "Order",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusReason",
                table: "Order");
        }
    }
}
