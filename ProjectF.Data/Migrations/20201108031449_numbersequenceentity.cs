using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectF.Data.Migrations
{
    public partial class numbersequenceentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Automatic",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "FinalSequence",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "ValidFrom",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "ValidTo",
                table: "DocumentNumberSequence");

            migrationBuilder.AddColumn<int>(
                name: "FinalSequency",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitialSequency",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IntialSequency",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "NextSequency",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalSequency",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "InitialSequency",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "IntialSequency",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "NextSequency",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "DocumentNumberSequence");

            migrationBuilder.AddColumn<bool>(
                name: "Automatic",
                table: "DocumentNumberSequence",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "DocumentNumberSequence",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "FinalSequence",
                table: "DocumentNumberSequence",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "DocumentNumberSequence",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Sequence",
                table: "DocumentNumberSequence",
                type: "bigint",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidFrom",
                table: "DocumentNumberSequence",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidTo",
                table: "DocumentNumberSequence",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
