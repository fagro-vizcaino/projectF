using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectF.Data.Migrations
{
    public partial class unitOfMeasureColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f2f6c50-e6e4-4ae4-b570-4f93fc1d1091");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce384bc2-8bb7-4e49-9a5f-4ede9fb3d701");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc09cd09-898f-4111-a6fe-b261513064c3");

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a1e5725-7e0b-4f04-a476-21c59bfddfbd", "cc0c1151-be8c-441f-9150-8089b7c47980", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d6834e79-d7d2-4971-9662-5b55ea465d7f", "c46fc176-8ad6-41d9-be69-7049a431de5e", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5b95009-f85f-4103-9e01-eb7b4b332ec5", "93801ac6-5c1b-4d0b-b0d7-d6f360256896", "Visitor", "Visitor" });

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "UnitOfMeasureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UnitOfMeasure_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UnitOfMeasureId",
                table: "Product");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a1e5725-7e0b-4f04-a476-21c59bfddfbd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5b95009-f85f-4103-9e01-eb7b4b332ec5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6834e79-d7d2-4971-9662-5b55ea465d7f");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "Product");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5f2f6c50-e6e4-4ae4-b570-4f93fc1d1091", "64f0c2b7-4f34-4bfa-ba5f-1dfff9fe733d", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dc09cd09-898f-4111-a6fe-b261513064c3", "dadf32a1-8f5e-4e47-a653-9a336a1a1715", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce384bc2-8bb7-4e49-9a5f-4ede9fb3d701", "f781d998-099d-4f91-91d1-7acad0cc7d6b", "Visitor", "Visitor" });
        }
    }
}
