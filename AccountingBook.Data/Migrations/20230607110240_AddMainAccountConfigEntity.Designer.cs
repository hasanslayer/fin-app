﻿// <auto-generated />
using System;
using AccountingBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccountingBook.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230607110240_AddMainAccountConfigEntity")]
    partial class AddMainAccountConfigEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AccountingBook.Core.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CompanyCode = "C001",
                            CompanyName = "hexquote.com",
                            ShortName = "hexquote"
                        });
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountCode")
                        .HasColumnType("int")
                        .HasMaxLength(5);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<int>("DrOrCrSide")
                        .HasColumnType("int");

                    b.Property<long?>("ParentAccountId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountCode")
                        .IsUnique();

                    b.HasIndex("ParentAccountId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AccountCode = 10000,
                            AccountName = "Assets",
                            AccountType = 1,
                            DrOrCrSide = 1
                        },
                        new
                        {
                            Id = 2L,
                            AccountCode = 20000,
                            AccountName = "Liabilities",
                            AccountType = 2,
                            DrOrCrSide = 2
                        },
                        new
                        {
                            Id = 3L,
                            AccountCode = 30000,
                            AccountName = "Equity",
                            AccountType = 3,
                            DrOrCrSide = 2
                        },
                        new
                        {
                            Id = 4L,
                            AccountCode = 40000,
                            AccountName = "Revenue",
                            AccountType = 4,
                            DrOrCrSide = 2
                        },
                        new
                        {
                            Id = 5L,
                            AccountCode = 50000,
                            AccountName = "Expense",
                            AccountType = 5,
                            DrOrCrSide = 1
                        },
                        new
                        {
                            Id = 6L,
                            AccountCode = 10111,
                            AccountName = "Regular Checking Account",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 7L,
                            AccountCode = 10112,
                            AccountName = "Savings Account",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 8L,
                            AccountCode = 10113,
                            AccountName = "Cash in Hand A/C",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 9L,
                            AccountCode = 10120,
                            AccountName = "Accounts Receivable",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 10L,
                            AccountCode = 10140,
                            AccountName = "Prepaid Expenses",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 11L,
                            AccountCode = 10150,
                            AccountName = "Employee Advances",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 12L,
                            AccountCode = 10800,
                            AccountName = "Inventory",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 13L,
                            AccountCode = 10810,
                            AccountName = "Goods Received Clearing Account",
                            AccountType = 1,
                            DrOrCrSide = 1,
                            ParentAccountId = 1L
                        },
                        new
                        {
                            Id = 14L,
                            AccountCode = 20110,
                            AccountName = "Account Payable",
                            AccountType = 2,
                            DrOrCrSide = 2,
                            ParentAccountId = 2L
                        },
                        new
                        {
                            Id = 15L,
                            AccountCode = 20120,
                            AccountName = "Customer Advances",
                            AccountType = 2,
                            DrOrCrSide = 2,
                            ParentAccountId = 2L
                        },
                        new
                        {
                            Id = 16L,
                            AccountCode = 20202,
                            AccountName = "Wages Payable",
                            AccountType = 2,
                            DrOrCrSide = 2,
                            ParentAccountId = 2L
                        },
                        new
                        {
                            Id = 17L,
                            AccountCode = 20300,
                            AccountName = "Sales Tax",
                            AccountType = 2,
                            DrOrCrSide = 2,
                            ParentAccountId = 2L
                        },
                        new
                        {
                            Id = 18L,
                            AccountCode = 30100,
                            AccountName = "Member Capital",
                            AccountType = 3,
                            DrOrCrSide = 2,
                            ParentAccountId = 3L
                        },
                        new
                        {
                            Id = 19L,
                            AccountCode = 30200,
                            AccountName = "Capital Surplus",
                            AccountType = 3,
                            DrOrCrSide = 2,
                            ParentAccountId = 3L
                        },
                        new
                        {
                            Id = 20L,
                            AccountCode = 30300,
                            AccountName = "Retained Surplus",
                            AccountType = 3,
                            DrOrCrSide = 2,
                            ParentAccountId = 3L
                        },
                        new
                        {
                            Id = 21L,
                            AccountCode = 30400,
                            AccountName = "Accumulated Profits",
                            AccountType = 3,
                            DrOrCrSide = 2,
                            ParentAccountId = 3L
                        },
                        new
                        {
                            Id = 22L,
                            AccountCode = 30500,
                            AccountName = "Accumulated Losses",
                            AccountType = 3,
                            DrOrCrSide = 2,
                            ParentAccountId = 3L
                        },
                        new
                        {
                            Id = 23L,
                            AccountCode = 40100,
                            AccountName = "Sales A/C",
                            AccountType = 4,
                            DrOrCrSide = 2,
                            ParentAccountId = 4L
                        },
                        new
                        {
                            Id = 24L,
                            AccountCode = 40200,
                            AccountName = "Sales Discounts",
                            AccountType = 4,
                            DrOrCrSide = 2,
                            ParentAccountId = 4L
                        },
                        new
                        {
                            Id = 25L,
                            AccountCode = 40500,
                            AccountName = "Shipping and Handling",
                            AccountType = 4,
                            DrOrCrSide = 2,
                            ParentAccountId = 4L
                        },
                        new
                        {
                            Id = 26L,
                            AccountCode = 50101,
                            AccountName = "Salary Expenses",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 27L,
                            AccountCode = 50200,
                            AccountName = "Purchase A/C",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 28L,
                            AccountCode = 50300,
                            AccountName = "Cost of Goods Sold",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 29L,
                            AccountCode = 50400,
                            AccountName = "Purchase Discounts",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 30L,
                            AccountCode = 50500,
                            AccountName = "Purchase price Variance",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 31L,
                            AccountCode = 50600,
                            AccountName = "Other Expenses",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        },
                        new
                        {
                            Id = 32L,
                            AccountCode = 50700,
                            AccountName = "Purchase Tax",
                            AccountType = 5,
                            DrOrCrSide = 1,
                            ParentAccountId = 5L
                        });
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.AccountConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FinancialDimensionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAllowNull")
                        .HasColumnType("bit");

                    b.Property<Guid>("MainAccountConfigId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FinancialDimensionId");

                    b.HasIndex("MainAccountConfigId");

                    b.ToTable("AccountConfigs");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.FinancialDimension", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinancialDimensions");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.FinancialDimensionValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DimensionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DimensionTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DimensionTypeId");

                    b.ToTable("FinancialDimensionValues");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.GeneralLedgerHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GeneralLedgerHeaders");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.GeneralLedgerLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DrCr")
                        .HasColumnType("int");

                    b.Property<long>("GeneralLedgerHeaderId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GeneralLedgerHeaderId");

                    b.ToTable("GeneralLedgerLines");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.JournalEntryHeader", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long?>("GeneralLedgerHeaderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Posted")
                        .HasColumnType("bit");

                    b.Property<string>("ReferenceNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralLedgerHeaderId");

                    b.ToTable("JournalEntryHeaders");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.JournalEntryLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DrCr")
                        .HasColumnType("int");

                    b.Property<long>("JournalEntryHeaderId")
                        .HasColumnType("bigint");

                    b.Property<string>("Memo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("JournalEntryHeaderId");

                    b.ToTable("JournalEntryLines");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.MainAccountConfig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("MainAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("RegexPattern")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MainAccountId");

                    b.ToTable("MainAccountConfigs");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.Account", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.Account", "ParentAccount")
                        .WithMany("ChildAccounts")
                        .HasForeignKey("ParentAccountId");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.AccountConfig", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.FinancialDimension", "FinancialDimension")
                        .WithMany("AccountConfigs")
                        .HasForeignKey("FinancialDimensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountingBook.Core.Financial.MainAccountConfig", "MainAccountConfig")
                        .WithMany("AccountConfigs")
                        .HasForeignKey("MainAccountConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.FinancialDimensionValue", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.FinancialDimension", "DimensionType")
                        .WithMany("FinancialDimensionValues")
                        .HasForeignKey("DimensionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.GeneralLedgerLine", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.Account", "Account")
                        .WithMany("GeneralLedgerLines")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountingBook.Core.Financial.GeneralLedgerHeader", "GeneralLedgerHeader")
                        .WithMany("GeneralLedgerLines")
                        .HasForeignKey("GeneralLedgerHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.JournalEntryHeader", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.GeneralLedgerHeader", "GeneralLedgerHeader")
                        .WithMany()
                        .HasForeignKey("GeneralLedgerHeaderId");
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.JournalEntryLine", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AccountingBook.Core.Financial.JournalEntryHeader", "JournalEntryHeader")
                        .WithMany("JournalEntryLines")
                        .HasForeignKey("JournalEntryHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountingBook.Core.Financial.MainAccountConfig", b =>
                {
                    b.HasOne("AccountingBook.Core.Financial.Account", "MainAccount")
                        .WithMany("MainAccountConfigs")
                        .HasForeignKey("MainAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
