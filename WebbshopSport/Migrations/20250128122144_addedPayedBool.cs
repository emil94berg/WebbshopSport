using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbshopSport.Migrations
{
    /// <inheritdoc />
    public partial class addedPayedBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Payed",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payed",
                table: "OrderItems");
        }
    }
}
