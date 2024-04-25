using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.DataBase
{
    /// <inheritdoc />
    public partial class ChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(9133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(8954));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(4858),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(6725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(8723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(9174));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(1161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(4772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(6862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(3006));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(4557),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(4442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(6718));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 661, DateTimeKind.Local).AddTicks(5085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(2061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(942),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(4979));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 660, DateTimeKind.Local).AddTicks(5869),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 727, DateTimeKind.Local).AddTicks(7696));

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(8954),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(6725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(9174),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(8723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(4772),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(1161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(3006),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(6862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 729, DateTimeKind.Local).AddTicks(1941),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(4557));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(6718),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(4442));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(2061),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 661, DateTimeKind.Local).AddTicks(5085));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 728, DateTimeKind.Local).AddTicks(4979),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(942));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 4, 19, 58, 37, 727, DateTimeKind.Local).AddTicks(7696),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 660, DateTimeKind.Local).AddTicks(5869));
        }
    }
}
