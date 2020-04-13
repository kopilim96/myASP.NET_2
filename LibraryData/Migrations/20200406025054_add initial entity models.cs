using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryData.Migrations
{
    public partial class addinitialentitymodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryBranchid",
                table: "Patrons",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "libraryCardid",
                table: "Patrons",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "libraryBranches",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    telephone = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    openDate = table.Column<DateTime>(nullable: false),
                    imageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libraryBranches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "libraryCards",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fees = table.Column<decimal>(nullable: false),
                    created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libraryCards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "branchHours",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    branchid = table.Column<int>(nullable: true),
                    dayOfWeek = table.Column<int>(nullable: false),
                    openTime = table.Column<int>(nullable: false),
                    closeTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branchHours", x => x.id);
                    table.ForeignKey(
                        name: "FK_branchHours_libraryBranches_branchid",
                        column: x => x.branchid,
                        principalTable: "libraryBranches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "libraryAssets",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false),
                    Statusid = table.Column<int>(nullable: true),
                    cost = table.Column<decimal>(nullable: false),
                    imageUrl = table.Column<string>(nullable: true),
                    numberOfCopies = table.Column<int>(nullable: false),
                    LibraryBranchid = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    isbn = table.Column<string>(nullable: true),
                    author = table.Column<string>(nullable: true),
                    deweyIndex = table.Column<string>(nullable: true),
                    director = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libraryAssets", x => x.id);
                    table.ForeignKey(
                        name: "FK_libraryAssets_libraryBranches_LibraryBranchid",
                        column: x => x.LibraryBranchid,
                        principalTable: "libraryBranches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_libraryAssets_statuses_Statusid",
                        column: x => x.Statusid,
                        principalTable: "statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checkOut",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetid = table.Column<int>(nullable: true),
                    LibraryCardid = table.Column<int>(nullable: true),
                    since = table.Column<DateTime>(nullable: false),
                    until = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkOut", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkOut_libraryAssets_LibraryAssetid",
                        column: x => x.LibraryAssetid,
                        principalTable: "libraryAssets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkOut_libraryCards_LibraryCardid",
                        column: x => x.LibraryCardid,
                        principalTable: "libraryCards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checkOutHistory",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libraryAssetid = table.Column<int>(nullable: true),
                    LibraryCardid = table.Column<int>(nullable: true),
                    checkOut = table.Column<DateTime>(nullable: false),
                    checkIn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkOutHistory", x => x.id);
                    table.ForeignKey(
                        name: "FK_checkOutHistory_libraryCards_LibraryCardid",
                        column: x => x.LibraryCardid,
                        principalTable: "libraryCards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_checkOutHistory_libraryAssets_libraryAssetid",
                        column: x => x.libraryAssetid,
                        principalTable: "libraryAssets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hold",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryAssetid = table.Column<int>(nullable: true),
                    LibraryCardid = table.Column<int>(nullable: true),
                    HoldPlaced = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hold", x => x.id);
                    table.ForeignKey(
                        name: "FK_hold_libraryAssets_LibraryAssetid",
                        column: x => x.LibraryAssetid,
                        principalTable: "libraryAssets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hold_libraryCards_LibraryCardid",
                        column: x => x.LibraryCardid,
                        principalTable: "libraryCards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_LibraryBranchid",
                table: "Patrons",
                column: "LibraryBranchid");

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_libraryCardid",
                table: "Patrons",
                column: "libraryCardid");

            migrationBuilder.CreateIndex(
                name: "IX_branchHours_branchid",
                table: "branchHours",
                column: "branchid");

            migrationBuilder.CreateIndex(
                name: "IX_checkOut_LibraryAssetid",
                table: "checkOut",
                column: "LibraryAssetid");

            migrationBuilder.CreateIndex(
                name: "IX_checkOut_LibraryCardid",
                table: "checkOut",
                column: "LibraryCardid");

            migrationBuilder.CreateIndex(
                name: "IX_checkOutHistory_LibraryCardid",
                table: "checkOutHistory",
                column: "LibraryCardid");

            migrationBuilder.CreateIndex(
                name: "IX_checkOutHistory_libraryAssetid",
                table: "checkOutHistory",
                column: "libraryAssetid");

            migrationBuilder.CreateIndex(
                name: "IX_hold_LibraryAssetid",
                table: "hold",
                column: "LibraryAssetid");

            migrationBuilder.CreateIndex(
                name: "IX_hold_LibraryCardid",
                table: "hold",
                column: "LibraryCardid");

            migrationBuilder.CreateIndex(
                name: "IX_libraryAssets_LibraryBranchid",
                table: "libraryAssets",
                column: "LibraryBranchid");

            migrationBuilder.CreateIndex(
                name: "IX_libraryAssets_Statusid",
                table: "libraryAssets",
                column: "Statusid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_libraryBranches_LibraryBranchid",
                table: "Patrons",
                column: "LibraryBranchid",
                principalTable: "libraryBranches",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_libraryCards_libraryCardid",
                table: "Patrons",
                column: "libraryCardid",
                principalTable: "libraryCards",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_libraryBranches_LibraryBranchid",
                table: "Patrons");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_libraryCards_libraryCardid",
                table: "Patrons");

            migrationBuilder.DropTable(
                name: "branchHours");

            migrationBuilder.DropTable(
                name: "checkOut");

            migrationBuilder.DropTable(
                name: "checkOutHistory");

            migrationBuilder.DropTable(
                name: "hold");

            migrationBuilder.DropTable(
                name: "libraryAssets");

            migrationBuilder.DropTable(
                name: "libraryCards");

            migrationBuilder.DropTable(
                name: "libraryBranches");

            migrationBuilder.DropTable(
                name: "statuses");

            migrationBuilder.DropIndex(
                name: "IX_Patrons_LibraryBranchid",
                table: "Patrons");

            migrationBuilder.DropIndex(
                name: "IX_Patrons_libraryCardid",
                table: "Patrons");

            migrationBuilder.DropColumn(
                name: "LibraryBranchid",
                table: "Patrons");

            migrationBuilder.DropColumn(
                name: "libraryCardid",
                table: "Patrons");
        }
    }
}
