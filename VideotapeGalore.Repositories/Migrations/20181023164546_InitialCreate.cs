using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VideotapeGalore.Repositories.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tapes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Eidr = table.Column<string>(nullable: true),
                    DirectorFirstName = table.Column<string>(nullable: true),
                    DirectorLastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tapes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowInfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BorrowDate = table.Column<DateTime>(nullable: false, defaultValueSql: "datetime('now')"),
                    ReturnDate = table.Column<DateTime>(nullable: true),
                    FriendId = table.Column<int>(nullable: false),
                    TapeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowInfos_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowInfos_Tapes_TapeId",
                        column: x => x.TapeId,
                        principalTable: "Tapes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    FriendId = table.Column<int>(nullable: false),
                    TapeId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.FriendId, x.TapeId });
                    table.ForeignKey(
                        name: "FK_Reviews_Friends_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Friends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Tapes_TapeId",
                        column: x => x.TapeId,
                        principalTable: "Tapes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowInfos_FriendId",
                table: "BorrowInfos",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowInfos_TapeId",
                table: "BorrowInfos",
                column: "TapeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TapeId",
                table: "Reviews",
                column: "TapeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowInfos");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Tapes");
        }
    }
}
