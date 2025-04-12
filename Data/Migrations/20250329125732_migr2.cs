using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.Data.Migrations
{
    /// <inheritdoc />
    public partial class migr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Products_ProductModelId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ProductModelId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "ProductModelTagModel",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductModelTagModel", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductModelTagModel_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductModelTagModel_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductModelTagModel_TagsId",
                table: "ProductModelTagModel",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductModelTagModel");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelId",
                table: "Tags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductModelId",
                table: "Tags",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Products_ProductModelId",
                table: "Tags",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
