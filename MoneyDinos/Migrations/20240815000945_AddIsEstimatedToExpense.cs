using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyDinos.Migrations
{
    public partial class AddIsEstimatedToExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEstimated",
                table: "Expenses",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEstimated",
                table: "Expenses");
        }
    }
}
