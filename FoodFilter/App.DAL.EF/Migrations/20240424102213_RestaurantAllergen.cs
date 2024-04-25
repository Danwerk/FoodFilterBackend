using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantAllergen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RestaurantAllergens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                    AllergenId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAllergens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestaurantAllergens_Allergens_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantAllergens_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAllergens_AllergenId",
                table: "RestaurantAllergens",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAllergens_RestaurantId",
                table: "RestaurantAllergens",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantAllergens");
        }
    }
}
