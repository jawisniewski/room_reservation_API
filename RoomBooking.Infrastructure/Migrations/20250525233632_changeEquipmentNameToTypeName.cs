using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoomBooking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeEquipmentNameToTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name_Name",
                table: "Equipment",
                newName: "Type_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type_Name",
                table: "Equipment",
                newName: "Name_Name");
        }
    }
}
