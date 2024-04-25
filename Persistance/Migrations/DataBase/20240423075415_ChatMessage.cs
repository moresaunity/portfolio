using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations.DataBase
{
    /// <inheritdoc />
    public partial class ChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 517, DateTimeKind.Local).AddTicks(4427),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(9133));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 516, DateTimeKind.Local).AddTicks(6673),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(4858));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 514, DateTimeKind.Local).AddTicks(6188),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(8723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 516, DateTimeKind.Local).AddTicks(3242),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(1161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 515, DateTimeKind.Local).AddTicks(9954),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(6862));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 515, DateTimeKind.Local).AddTicks(3080),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(4557));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 514, DateTimeKind.Local).AddTicks(104),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(4442));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 512, DateTimeKind.Local).AddTicks(5746),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 661, DateTimeKind.Local).AddTicks(5085));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 513, DateTimeKind.Local).AddTicks(3789),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(942));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 511, DateTimeKind.Local).AddTicks(9682),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 660, DateTimeKind.Local).AddTicks(5869));

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatRoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(9133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 517, DateTimeKind.Local).AddTicks(4427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(4858),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 516, DateTimeKind.Local).AddTicks(6673));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(8723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 514, DateTimeKind.Local).AddTicks(6188));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 664, DateTimeKind.Local).AddTicks(1161),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 516, DateTimeKind.Local).AddTicks(3242));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(6862),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 515, DateTimeKind.Local).AddTicks(9954));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductItemFavourites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 663, DateTimeKind.Local).AddTicks(4557),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 515, DateTimeKind.Local).AddTicks(3080));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "ProductBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(4442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 514, DateTimeKind.Local).AddTicks(104));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 661, DateTimeKind.Local).AddTicks(5085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 512, DateTimeKind.Local).AddTicks(5746));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 662, DateTimeKind.Local).AddTicks(942),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 513, DateTimeKind.Local).AddTicks(3789));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Discounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 22, 18, 53, 35, 660, DateTimeKind.Local).AddTicks(5869),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 23, 11, 24, 14, 511, DateTimeKind.Local).AddTicks(9682));
        }
    }
}
