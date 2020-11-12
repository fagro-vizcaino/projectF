using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectF.Data.Migrations
{
    public partial class numbersequenceentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NcfType",
                table: "InvoiceHeader");

            migrationBuilder.DropColumn(
                name: "Automatic",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "DocumentType",
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

            migrationBuilder.AlterColumn<string>(
                name: "Ncf",
                table: "InvoiceHeader",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "NumberSequenceId",
                table: "InvoiceHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "FinalSequence",
                table: "DocumentNumberSequence",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<int>(
                name: "InitialSequence",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "DocumentNumberSequence",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<int>(
                name: "NextSequence",
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
                name: "NumberSequenceId",
                table: "InvoiceHeader");

            migrationBuilder.DropColumn(
                name: "InitialSequence",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "NextSequence",
                table: "DocumentNumberSequence");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "DocumentNumberSequence");

            migrationBuilder.AlterColumn<long>(
                name: "Ncf",
                table: "InvoiceHeader",
                type: "bigint",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NcfType",
                table: "InvoiceHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FinalSequence",
                table: "DocumentNumberSequence",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

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
