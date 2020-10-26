﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectF.Data.Context;

namespace ProjectF.Data.Migrations
{
    [DbContext(typeof(_AppDbContext))]
    [Migration("20201016005649_taxColumToProducts")]
    partial class taxColumToProducts
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectF.Data.Entities.Auth.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Banks.BankAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BankAccountId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(18)")
                        .HasMaxLength(18);

                    b.Property<long?>("BankAccountTypeId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(220)")
                        .HasMaxLength(220);

                    b.Property<decimal>("InitialBalance")
                        .HasColumnType("decimal(16,2)");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("Datetime");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountTypeId");

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Banks.BankAccountType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("BankAccountTypeId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(119)")
                        .HasMaxLength(119);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("BankAccountType");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Categories.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<bool>("ShowOn")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Clients.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClientId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("Date");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("HomeOrApartment")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<string>("Rnc")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("HomeOrApartment")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<long?>("RegimeTypeId")
                        .HasColumnType("bigint");

                    b.Property<string>("Rnc")
                        .HasColumnType("nvarchar(18)")
                        .HasMaxLength(18);

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(220)")
                        .HasMaxLength(220);

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("RegimeTypeId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Countries.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CountryId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("IconImage")
                        .HasColumnType("nvarchar(800)")
                        .HasMaxLength(800);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IconImage = "",
                            Name = "Dominican Republic"
                        },
                        new
                        {
                            Id = 2,
                            IconImage = "",
                            Name = "Puerto Rico"
                        },
                        new
                        {
                            Id = 3,
                            IconImage = "",
                            Name = "Panama"
                        },
                        new
                        {
                            Id = 4,
                            IconImage = "",
                            Name = "Colombia"
                        });
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Currencies.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CurrencyId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.ToTable("Currency");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "DOP Peso Dominicano"
                        },
                        new
                        {
                            Id = 2,
                            Name = "US Dolar USA"
                        });
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Invoices.InvoiceDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InvoiceDetailId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(16,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<long?>("InvoiceHeaderId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProductCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.Property<decimal>("TaxPercent")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceHeaderId");

                    b.ToTable("InvoiceDetail");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Invoices.InvoiceHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InvoiceHeaderId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<long?>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Footer")
                        .HasColumnType("nvarchar(220)")
                        .HasMaxLength(220);

                    b.Property<long>("Ncf")
                        .HasColumnType("bigint")
                        .HasMaxLength(20);

                    b.Property<string>("NcfType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(220)")
                        .HasMaxLength(220);

                    b.Property<long?>("PaymentTermId")
                        .HasColumnType("bigint");

                    b.Property<string>("Rnc")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime>("SystemCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SystemModified")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TaxTotal")
                        .HasColumnType("decimal(12,2)");

                    b.Property<string>("TermAndConditions")
                        .HasColumnType("nvarchar(220)")
                        .HasMaxLength(220);

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("PaymentTermId");

                    b.ToTable("InvoiceHeader");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.PaymentList.PaymentTerm", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentTermId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DayValue")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(120)")
                        .HasMaxLength(120);

                    b.HasKey("Id");

                    b.ToTable("PaymentTerm");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Sequences.DocumentNumberSequence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DocumentNumberSequenceId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Automatic")
                        .HasColumnType("bit");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<long>("FinalSequence")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDefault")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<long?>("Sequence")
                        .HasColumnType("bigint")
                        .HasMaxLength(60);

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DocumentNumberSequence");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Suppliers.Supplier", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SupplierId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("HomeOrApartment")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<bool>("IsIndependent")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<string>("Rnc")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType.TaxRegimeType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TaxRegimeTypeId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("TaxRegimeType");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Taxes.Tax", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TaxId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<decimal>("PercentValue")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id");

                    b.ToTable("Tax");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Werehouses.Werehouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("WerehouseId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Location")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Werehouse");
                });

            modelBuilder.Entity("ProjectF.Data.Products.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductId")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(16,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<bool>("IsService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("Price2")
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("Price3")
                        .HasColumnType("decimal(16,2)");

                    b.Property<decimal>("Price4")
                        .HasColumnType("decimal(16,2)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<long?>("TaxId")
                        .HasColumnType("bigint");

                    b.Property<long?>("WerehouseId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TaxId");

                    b.HasIndex("WerehouseId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Auth.User", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Countries.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Banks.BankAccount", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Banks.BankAccountType", "BankAccountType")
                        .WithMany()
                        .HasForeignKey("BankAccountTypeId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Clients.Client", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Countries.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Company", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Countries.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("ProjectF.Data.Entities.Currencies.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.HasOne("ProjectF.Data.Entities.Taxes.BusinessTaxRegimeType.TaxRegimeType", "RegimeType")
                        .WithMany()
                        .HasForeignKey("RegimeTypeId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Invoices.InvoiceDetail", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Invoices.InvoiceHeader", "InvoiceHeader")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceHeaderId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Invoices.InvoiceHeader", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Clients.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("ProjectF.Data.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("ProjectF.Data.Entities.PaymentList.PaymentTerm", "PaymentTerm")
                        .WithMany()
                        .HasForeignKey("PaymentTermId");
                });

            modelBuilder.Entity("ProjectF.Data.Entities.Suppliers.Supplier", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Countries.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("ProjectF.Data.Products.Product", b =>
                {
                    b.HasOne("ProjectF.Data.Entities.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectF.Data.Entities.Taxes.Tax", "Tax")
                        .WithMany()
                        .HasForeignKey("TaxId");

                    b.HasOne("ProjectF.Data.Entities.Werehouses.Werehouse", "Werehouse")
                        .WithMany("Products")
                        .HasForeignKey("WerehouseId");
                });
#pragma warning restore 612, 618
        }
    }
}