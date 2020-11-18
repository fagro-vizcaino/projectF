﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectF.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccountType",
                columns: table => new
                {
                    BankAccountTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 119, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountType", x => x.BankAccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    ShowOn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    IconImage = table.Column<string>(maxLength: 800, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentNumberSequence",
                columns: table => new
                {
                    DocumentNumberSequenceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Prefix = table.Column<string>(maxLength: 20, nullable: false),
                    InitialSequence = table.Column<int>(nullable: false),
                    NextSequence = table.Column<int>(nullable: false),
                    FinalSequence = table.Column<int>(nullable: false),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentNumberSequence", x => x.DocumentNumberSequenceId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                columns: table => new
                {
                    PaymentTermId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 120, nullable: false),
                    DayValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.PaymentTermId);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    PercentValue = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "TaxRegimeType",
                columns: table => new
                {
                    TaxRegimeTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRegimeType", x => x.TaxRegimeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Werehouse",
                columns: table => new
                {
                    WerehouseId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Location = table.Column<string>(maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Werehouse", x => x.WerehouseId);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    BankAccountId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountName = table.Column<string>(maxLength: 60, nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 18, nullable: false),
                    Description = table.Column<string>(maxLength: 220, nullable: false),
                    InitialBalance = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    BankAccountTypeId = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(type: "Datetime", nullable: false),
                    Modified = table.Column<DateTime>(type: "Datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankAccountType_BankAccountTypeId",
                        column: x => x.BankAccountTypeId,
                        principalTable: "BankAccountType",
                        principalColumn: "BankAccountTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Firstname = table.Column<string>(maxLength: 60, nullable: false),
                    Lastname = table.Column<string>(maxLength: 120, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    Phone = table.Column<string>(maxLength: 11, nullable: false),
                    Birthday = table.Column<DateTime>(type: "Date", nullable: true),
                    Rnc = table.Column<string>(maxLength: 15, nullable: false),
                    HomeOrApartment = table.Column<string>(maxLength: 200, nullable: false),
                    City = table.Column<string>(maxLength: 60, nullable: false),
                    Street = table.Column<string>(maxLength: 60, nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Client_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 60, nullable: false),
                    Phone = table.Column<string>(maxLength: 11, nullable: false),
                    Rnc = table.Column<string>(maxLength: 15, nullable: false),
                    HomeOrApartment = table.Column<string>(maxLength: 200, nullable: false),
                    City = table.Column<string>(maxLength: 60, nullable: false),
                    Street = table.Column<string>(maxLength: 60, nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    IsIndependent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_Supplier_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    CountryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Rnc = table.Column<string>(maxLength: 18, nullable: false),
                    HomeOrApartment = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 30, nullable: false),
                    Street = table.Column<string>(maxLength: 220, nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    Phone = table.Column<string>(maxLength: 11, nullable: false),
                    Website = table.Column<string>(nullable: false),
                    RegimeTypeId = table.Column<long>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_Company_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_TaxRegimeType_RegimeTypeId",
                        column: x => x.RegimeTypeId,
                        principalTable: "TaxRegimeType",
                        principalColumn: "TaxRegimeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(maxLength: 60, nullable: false),
                    Reference = table.Column<string>(maxLength: 60, nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    WerehouseId = table.Column<long>(nullable: false),
                    TaxId = table.Column<long>(nullable: false),
                    IsService = table.Column<bool>(nullable: false, defaultValue: false),
                    Cost = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Price2 = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Price3 = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Price4 = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Werehouse_WerehouseId",
                        column: x => x.WerehouseId,
                        principalTable: "Werehouse",
                        principalColumn: "WerehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceHeader",
                columns: table => new
                {
                    InvoiceHeaderId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    Ncf = table.Column<string>(maxLength: 20, nullable: false),
                    NumberSequenceId = table.Column<int>(nullable: false),
                    Rnc = table.Column<string>(maxLength: 15, nullable: false),
                    ClientId = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PaymentTermId = table.Column<long>(nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    TaxTotal = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Notes = table.Column<string>(maxLength: 220, nullable: false),
                    TermAndConditions = table.Column<string>(maxLength: 220, nullable: false),
                    Footer = table.Column<string>(maxLength: 220, nullable: false),
                    SystemCreated = table.Column<DateTime>(nullable: false),
                    SystemModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceHeader", x => x.InvoiceHeaderId);
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerm",
                        principalColumn: "PaymentTermId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetail",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductCode = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 60, nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    TaxPercent = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    InvoiceHeaderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceDetail", x => x.InvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_InvoiceDetail_InvoiceHeader_InvoiceHeaderId",
                        column: x => x.InvoiceHeaderId,
                        principalTable: "InvoiceHeader",
                        principalColumn: "InvoiceHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "CountryId", "IconImage", "Name" },
                values: new object[,]
                {
                    { 1, "", "Dominican Republic" },
                    { 2, "", "Puerto Rico" },
                    { 3, "", "Panama" },
                    { 4, "", "Colombia" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "CurrencyId", "Name" },
                values: new object[,]
                {
                    { 1, "DOP Peso Dominicano" },
                    { 2, "US Dolar USA" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankAccountTypeId",
                table: "BankAccount",
                column: "BankAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_CountryId",
                table: "Client",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CountryId",
                table: "Company",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_CurrencyId",
                table: "Company",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_RegimeTypeId",
                table: "Company",
                column: "RegimeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetail_InvoiceHeaderId",
                table: "InvoiceDetail",
                column: "InvoiceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_ClientId",
                table: "InvoiceHeader",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_PaymentTermId",
                table: "InvoiceHeader",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_TaxId",
                table: "Product",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_WerehouseId",
                table: "Product",
                column: "WerehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CountryId",
                table: "Supplier",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                table: "User",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "DocumentNumberSequence");

            migrationBuilder.DropTable(
                name: "InvoiceDetail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BankAccountType");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "TaxRegimeType");

            migrationBuilder.DropTable(
                name: "InvoiceHeader");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Werehouse");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "PaymentTerm");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}