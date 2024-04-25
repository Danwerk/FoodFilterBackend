using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RestaurantClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClaim_Claims_ClaimId",
                table: "RestaurantClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClaim_Restaurants_RestaurantId",
                table: "RestaurantClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantClaim",
                table: "RestaurantClaim");

            migrationBuilder.RenameTable(
                name: "RestaurantClaim",
                newName: "RestaurantClaims");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantClaim_RestaurantId",
                table: "RestaurantClaims",
                newName: "IX_RestaurantClaims_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantClaim_ClaimId",
                table: "RestaurantClaims",
                newName: "IX_RestaurantClaims_ClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantClaims",
                table: "RestaurantClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClaims_Claims_ClaimId",
                table: "RestaurantClaims",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClaims_Restaurants_RestaurantId",
                table: "RestaurantClaims",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClaims_Claims_ClaimId",
                table: "RestaurantClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantClaims_Restaurants_RestaurantId",
                table: "RestaurantClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantClaims",
                table: "RestaurantClaims");

            migrationBuilder.RenameTable(
                name: "RestaurantClaims",
                newName: "RestaurantClaim");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantClaims_RestaurantId",
                table: "RestaurantClaim",
                newName: "IX_RestaurantClaim_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_RestaurantClaims_ClaimId",
                table: "RestaurantClaim",
                newName: "IX_RestaurantClaim_ClaimId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantClaim",
                table: "RestaurantClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClaim_Claims_ClaimId",
                table: "RestaurantClaim",
                column: "ClaimId",
                principalTable: "Claims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantClaim_Restaurants_RestaurantId",
                table: "RestaurantClaim",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
