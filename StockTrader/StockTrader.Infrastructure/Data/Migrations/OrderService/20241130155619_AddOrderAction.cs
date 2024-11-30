using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTrader.Infrastructure.Data.Migrations.OrderService
{
    /// <inheritdoc />
    public partial class AddOrderAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderAction",
                table: "Order",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderAction",
                table: "Order");
        }
    }
}
