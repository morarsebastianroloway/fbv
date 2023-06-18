using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FBV.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingSlip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ShippingSlip",
                table: "PurchaseOrders",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhysical",
                table: "PurchaseOrderLines",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingSlip",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IsPhysical",
                table: "PurchaseOrderLines");
        }
    }
}
