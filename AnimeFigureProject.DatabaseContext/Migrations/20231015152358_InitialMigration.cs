using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeFigureProject.DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    TotalValue = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthenticationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Origins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeFigures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    YearOfRelease = table.Column<int>(type: "int", nullable: true),
                    ImageFolderPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeFigures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeFigures_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnimeFigures_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnimeFigures_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionCollector",
                columns: table => new
                {
                    CollectionsId = table.Column<int>(type: "int", nullable: false),
                    CollectorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionCollector", x => new { x.CollectionsId, x.CollectorsId });
                    table.ForeignKey(
                        name: "FK_CollectionCollector_Collections_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionCollector_Collectors_CollectorsId",
                        column: x => x.CollectorsId,
                        principalTable: "Collectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimeFigureOrigin",
                columns: table => new
                {
                    AnimeFiguresId = table.Column<int>(type: "int", nullable: false),
                    OriginsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeFigureOrigin", x => new { x.AnimeFiguresId, x.OriginsId });
                    table.ForeignKey(
                        name: "FK_AnimeFigureOrigin_AnimeFigures_AnimeFiguresId",
                        column: x => x.AnimeFiguresId,
                        principalTable: "AnimeFigures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeFigureOrigin_Origins_OriginsId",
                        column: x => x.OriginsId,
                        principalTable: "Origins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: true),
                    AnimeFigureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AnimeFigures_AnimeFigureId",
                        column: x => x.AnimeFigureId,
                        principalTable: "AnimeFigures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Collectors_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Collectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeFigureOrigin_OriginsId",
                table: "AnimeFigureOrigin",
                column: "OriginsId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeFigures_BrandId",
                table: "AnimeFigures",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeFigures_CategoryId",
                table: "AnimeFigures",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeFigures_CollectionId",
                table: "AnimeFigures",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionCollector_CollectorsId",
                table: "CollectionCollector",
                column: "CollectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AnimeFigureId",
                table: "Reviews",
                column: "AnimeFigureId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_OwnerId",
                table: "Reviews",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeFigureOrigin");

            migrationBuilder.DropTable(
                name: "CollectionCollector");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Origins");

            migrationBuilder.DropTable(
                name: "AnimeFigures");

            migrationBuilder.DropTable(
                name: "Collectors");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Collections");
        }
    }
}
