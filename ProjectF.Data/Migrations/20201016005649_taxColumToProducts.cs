using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectF.Data.Migrations
{
    public partial class taxColumToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaxId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxId",
                table: "Product",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tax_TaxId",
                table: "Product",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Tax_TaxId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_TaxId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TaxId",
                table: "Product");
        }
    }
}
