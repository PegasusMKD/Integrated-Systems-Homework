using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISH.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "viewSlots",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeSlot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewSlots", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_viewSlots_MovieGenre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "MovieGenre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_carts_User_UserGuid",
                        column: x => x.UserGuid,
                        principalTable: "User",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<int>(type: "int", nullable: false),
                    OrderedByGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_orders_User_OrderedByGuid",
                        column: x => x.OrderedByGuid,
                        principalTable: "User",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    ViewSlotGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BoughtByGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_tickets_User_BoughtByGuid",
                        column: x => x.BoughtByGuid,
                        principalTable: "User",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_tickets_carts_CartGuid",
                        column: x => x.CartGuid,
                        principalTable: "carts",
                        principalColumn: "Guid");
                    table.ForeignKey(
                        name: "FK_tickets_viewSlots_ViewSlotGuid",
                        column: x => x.ViewSlotGuid,
                        principalTable: "viewSlots",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketPrice = table.Column<int>(type: "int", nullable: false),
                    ItemPrice = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TimeSlot = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_OrderGuid",
                        column: x => x.OrderGuid,
                        principalTable: "orders",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carts_UserGuid",
                table: "carts",
                column: "UserGuid");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_Name",
                table: "MovieGenre",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderGuid",
                table: "orderItems",
                column: "OrderGuid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderedByGuid",
                table: "orders",
                column: "OrderedByGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_BoughtByGuid",
                table: "tickets",
                column: "BoughtByGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_CartGuid",
                table: "tickets",
                column: "CartGuid");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ViewSlotGuid",
                table: "tickets",
                column: "ViewSlotGuid");

            migrationBuilder.CreateIndex(
                name: "IX_viewSlots_GenreId",
                table: "viewSlots",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "viewSlots");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "MovieGenre");
        }
    }
}
