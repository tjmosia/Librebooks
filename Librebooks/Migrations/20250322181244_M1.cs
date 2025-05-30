﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountCashFlowType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCashFlowType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessSector",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", nullable: true),
                    Mobile = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    DialingCode = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWriteOff",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWriteOff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DateFormat",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Format = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPrintTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DocumentType = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPrintTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingMethod",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingTerm",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ShortName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemCompanyNumber",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    NextNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    NumberFormat = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemCompanyNumber", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    System = table.Column<bool>(type: "INTEGER", nullable: false),
                    Group = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Birthday = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Photo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DateRegistered = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateLastLoggedIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LoginHash = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshSecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshLoginHash = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ClassType = table.Column<string>(type: "TEXT", nullable: true),
                    CashFlowTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountCategory_AccountCashFlowType_CashFlowTypeId",
                        column: x => x.CashFlowTypeId,
                        principalTable: "AccountCashFlowType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Actionable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatorId = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Note_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ParentAccountId = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    System = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AccountCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Account_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BankName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    BranchName = table.Column<string>(type: "TEXT", nullable: true),
                    BranchCode = table.Column<string>(type: "TEXT", nullable: true),
                    SwiftCode = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankAccountCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BankAccountCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UniqueNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LegalName = table.Column<string>(type: "TEXT", nullable: true),
                    TradingName = table.Column<string>(type: "TEXT", nullable: true),
                    RegNumber = table.Column<string>(type: "TEXT", nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PostalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PhysicalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    FaxNumber = table.Column<string>(type: "TEXT", nullable: true),
                    YearsInBusiness = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessSectorId = table.Column<string>(type: "TEXT", nullable: true),
                    LogoId = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultBankAccountCompanyId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_BusinessSector_BusinessSectorId",
                        column: x => x.BusinessSectorId,
                        principalTable: "BusinessSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefaultBankAccount",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    BankAccountId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDefaultBankAccount", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyDefaultBankAccount_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDefaultBankAccount_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyImage",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DateCreated = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyImage_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMailSettings",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    SmtpServerName = table.Column<string>(type: "TEXT", nullable: true),
                    SmtpPort = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMailSettings", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyMailSettings_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRegionalSettings",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: false),
                    CurrencyCode = table.Column<string>(type: "TEXT", nullable: false),
                    DecimalMark = table.Column<string>(type: "TEXT", nullable: true),
                    ThousandsSeperator = table.Column<string>(type: "TEXT", nullable: true),
                    DateFormatId = table.Column<string>(type: "TEXT", nullable: false),
                    RoundToNearest = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRegionalSettings", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSettings_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSettings_Country_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Country",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSettings_Currency_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalTable: "Currency",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSettings_DateFormat_DateFormatId",
                        column: x => x.DateFormatId,
                        principalTable: "DateFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTaxType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTaxType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTaxType_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyTaxType_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSetup",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    NextNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    LeadingZeros = table.Column<short>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSetup", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CustomerSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSetup",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    NextNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    LeadingZeros = table.Column<short>(type: "INTEGER", nullable: false),
                    FooterMessage = table.Column<string>(type: "TEXT", nullable: true),
                    NoteMessage = table.Column<string>(type: "TEXT", nullable: true),
                    PrintTemplateId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentSetup_DocumentPrintTemplate_PrintTemplateId",
                        column: x => x.PrintTemplateId,
                        principalTable: "DocumentPrintTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    ParentId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemCategory_ItemCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemSetup",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    LeadingZeros = table.Column<short>(type: "INTEGER", nullable: false),
                    NextNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSetup", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_ItemSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierSetup",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    NextNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    LeadingZeros = table.Column<short>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierSetup", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_SupplierSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLogo",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    ImageId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLogo", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyLogo_CompanyImage_ImageId",
                        column: x => x.ImageId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyLogo_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefaultTaxType",
                columns: table => new
                {
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyTaxTypeId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDefaultTaxType", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyDefaultTaxType_CompanyTaxType_CompanyTaxTypeId",
                        column: x => x.CompanyTaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompanyDefaultTaxType_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    DebitAccountId = table.Column<string>(type: "TEXT", nullable: false),
                    CreditAccountId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Posted = table.Column<bool>(type: "INTEGER", nullable: false),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journal_Account_CreditAccountId",
                        column: x => x.CreditAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journal_Account_DebitAccountId",
                        column: x => x.DebitAccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journal_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Journal_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseLine",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IsItemType = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemCode = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    DocumentId = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseLine_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseBuyer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    ContactId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyUserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseBuyer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseBuyer_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseBuyer_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseBuyer_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesPerson",
                columns: table => new
                {
                    ContactId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyUserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesPerson", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_SalesPerson_CompanyUser_CompanyUserId",
                        column: x => x.CompanyUserId,
                        principalTable: "CompanyUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesPerson_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesPerson_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    Physical = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.UniqueConstraint("AK_Item_Code", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Item_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Item_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_ItemCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    RegisteredName = table.Column<string>(type: "TEXT", nullable: true),
                    TradingName = table.Column<string>(type: "TEXT", nullable: true),
                    VendorNumber = table.Column<string>(type: "TEXT", nullable: true),
                    VATRegNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Fax = table.Column<string>(type: "TEXT", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PhysicalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PostalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PaymentTermId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supplier_SupplierCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "SupplierCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalNote",
                columns: table => new
                {
                    JournalId = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalNote", x => new { x.JournalId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_JournalNote_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemAdjustment",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    OldQuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    FromSales = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAdjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAdjustment_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemAdjustment_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInventory",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    MinQuantityAllowed = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    MaxQuantityAllowed = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInventory", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_ItemInventory_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesLine",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IsItemType = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemCode = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxTypeId = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesLine_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SalesLine_Item_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Item",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocumentSupplierDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierName = table.Column<string>(type: "TEXT", nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    PhysicalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocumentSupplierDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDocumentSupplierDetails_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAdjustment",
                columns: table => new
                {
                    JournalId = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAdjustment", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_SupplierAdjustment_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierAdjustment_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierAdjustment_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContact",
                columns: table => new
                {
                    ContactId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_SupplierContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierContact_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierNote",
                columns: table => new
                {
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierNote", x => new { x.SupplierId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_SupplierNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierNote_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocument",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    SuppplierReference = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Footer = table.Column<string>(type: "TEXT", nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    IsDraft = table.Column<bool>(type: "INTEGER", nullable: false),
                    Printed = table.Column<bool>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<string>(type: "TEXT", nullable: false),
                    LogoId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_CompanyImage_LogoId",
                        column: x => x.LogoId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_DocumentStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DocumentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_PurchaseDocumentSupplierDetails_SupplierDetailsId",
                        column: x => x.SupplierDetailsId,
                        principalTable: "PurchaseDocumentSupplierDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceipt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Reconciled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    BankAccountId = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "TEXT", nullable: false),
                    LogoId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_CompanyImage_LogoId",
                        column: x => x.LogoId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_PurchaseDocumentSupplierDetails_SupplierDetailsId",
                        column: x => x.SupplierDetailsId,
                        principalTable: "PurchaseDocumentSupplierDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReceipt_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAccountsContact",
                columns: table => new
                {
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierContactId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAccountsContact", x => new { x.SupplierId, x.SupplierContactId });
                    table.ForeignKey(
                        name: "FK_SupplierAccountsContact_SupplierContact_SupplierContactId",
                        column: x => x.SupplierContactId,
                        principalTable: "SupplierContact",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierAccountsContact_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocumentLine",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    LineId = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocumentLine", x => new { x.DocumentId, x.LineId });
                    table.ForeignKey(
                        name: "FK_PurchaseDocumentLine_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDocumentLine_PurchaseLine_LineId",
                        column: x => x.LineId,
                        principalTable: "PurchaseLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocumentNote",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocumentNote", x => new { x.DocumentId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_PurchaseDocumentNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDocumentNote_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoice",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoice", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturn",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturn", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceReceipt",
                columns: table => new
                {
                    ReceiptId = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceDocumentId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceReceipt", x => new { x.ReceiptId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceReceipt_PurchaseInvoice_InvoiceDocumentId",
                        column: x => x.InvoiceDocumentId,
                        principalTable: "PurchaseInvoice",
                        principalColumn: "DocumentId");
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceReceipt_PurchaseInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "PurchaseInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceReceipt_PurchaseReceipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "PurchaseReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderInvoice",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderInvoice", x => new { x.OrderId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_PurchaseOrderInvoice_PurchaseInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "PurchaseInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderInvoice_PurchaseOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInvoiceReturn",
                columns: table => new
                {
                    ReturnId = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceReturn", x => new { x.InvoiceId, x.ReturnId });
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceReturn_PurchaseInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "PurchaseInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoiceReturn_PurchaseReturn_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "PurchaseReturn",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    LegalName = table.Column<string>(type: "TEXT", nullable: true),
                    TradingName = table.Column<string>(type: "TEXT", nullable: true),
                    DeliveryAddress = table.Column<string>(type: "TEXT", nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ValueAddedTaxNumber = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PaymentTermsInDays = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTermsFrom = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPersonId = table.Column<string>(type: "TEXT", nullable: true),
                    AcceptsElectronicInvoices = table.Column<bool>(type: "INTEGER", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingTermId = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingMethodId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_SalesPerson_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPerson",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Customer_ShippingTerm_ShippingTermId",
                        column: x => x.ShippingTermId,
                        principalTable: "ShippingTerm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAdjustment",
                columns: table => new
                {
                    JournalId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAdjustment", x => x.JournalId);
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_Journal_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCategory_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    ContactId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContact", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerNote",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNote", x => new { x.CustomerId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_CustomerNote_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocumentCustomerDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocumentCustomerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDocumentCustomerDetails_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccountsContact",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerContactId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccountsContact", x => new { x.CustomerId, x.CustomerContactId });
                    table.ForeignKey(
                        name: "FK_CustomerAccountsContact_CustomerContact_CustomerContactId",
                        column: x => x.CustomerContactId,
                        principalTable: "CustomerContact",
                        principalColumn: "ContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerAccountsContact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocument",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerReference = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    FooterMessage = table.Column<string>(type: "TEXT", nullable: true),
                    TaxExempt = table.Column<bool>(type: "INTEGER", nullable: false),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    SalesPersonId = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingTermId = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingMethodId = table.Column<string>(type: "TEXT", nullable: true),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<string>(type: "TEXT", nullable: false),
                    Printed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatorId = table.Column<string>(type: "TEXT", nullable: true),
                    LogoId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDocument_CompanyImage_LogoId",
                        column: x => x.LogoId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_DocumentStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DocumentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_SalesDocumentCustomerDetails_CustomerDetailsId",
                        column: x => x.CustomerDetailsId,
                        principalTable: "SalesDocumentCustomerDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_SalesPerson_SalesPersonId",
                        column: x => x.SalesPersonId,
                        principalTable: "SalesPerson",
                        principalColumn: "ContactId");
                    table.ForeignKey(
                        name: "FK_SalesDocument_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesDocument_ShippingTerm_ShippingTermId",
                        column: x => x.ShippingTermId,
                        principalTable: "ShippingTerm",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesDocument_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SalesReceipt",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    CustomerDetailsId = table.Column<string>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    Comments = table.Column<string>(type: "TEXT", nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reconciled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "TEXT", nullable: false),
                    LogoId = table.Column<string>(type: "TEXT", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_BankAccount_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_CompanyImage_LogoId",
                        column: x => x.LogoId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesReceipt_SalesDocumentCustomerDetails_CustomerDetailsId",
                        column: x => x.CustomerDetailsId,
                        principalTable: "SalesDocumentCustomerDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesCredit",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCredit", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SalesCredit_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesCredit_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesCredit_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocumentLine",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    LineId = table.Column<string>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocumentLine", x => new { x.DocumentId, x.LineId });
                    table.ForeignKey(
                        name: "FK_SalesDocumentLine_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDocumentLine_SalesLine_LineId",
                        column: x => x.LineId,
                        principalTable: "SalesLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesDocumentNote",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    NoteId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocumentNote", x => new { x.DocumentId, x.NoteId });
                    table.ForeignKey(
                        name: "FK_SalesDocumentNote_Note_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Note",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesDocumentNote_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoice",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerWriteOffId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoice", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_CustomerWriteOff_CustomerWriteOffId",
                        column: x => x.CustomerWriteOffId,
                        principalTable: "CustomerWriteOff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesInvoice_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrder", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrder_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesQuote",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesQuote", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SalesQuote_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesQuote_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesQuote_SalesDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "SalesDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceCredit",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false),
                    CreditId = table.Column<string>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceCredit", x => new { x.InvoiceId, x.CreditId });
                    table.ForeignKey(
                        name: "FK_SalesInvoiceCredit_SalesCredit_CreditId",
                        column: x => x.CreditId,
                        principalTable: "SalesCredit",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceCredit_SalesInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoiceReceipt",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiptId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceReceipt", x => new { x.InvoiceId, x.ReceiptId });
                    table.ForeignKey(
                        name: "FK_SalesInvoiceReceipt_SalesInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceReceipt_SalesReceipt_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "SalesReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderInvoice",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderInvoice", x => new { x.OrderId, x.InvoiceId });
                    table.ForeignKey(
                        name: "FK_SalesOrderInvoice_SalesInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesOrderInvoice_SalesOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesQuoteOrder",
                columns: table => new
                {
                    QuoteId = table.Column<string>(type: "TEXT", nullable: false),
                    OrderId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesQuoteOrder", x => new { x.QuoteId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_SalesQuoteOrder_SalesOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesQuoteOrder_SalesQuote_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "SalesQuote",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_CategoryId",
                table: "Account",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CompanyId_CategoryId",
                table: "Account",
                columns: new[] { "CompanyId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentAccountId",
                table: "Account",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_TaxTypeId",
                table: "Account",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountCategory_CashFlowTypeId",
                table: "AccountCategory",
                column: "CashFlowTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CategoryId",
                table: "BankAccount",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_CompanyId",
                table: "BankAccount",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_PaymentMethodId",
                table: "BankAccount",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_BusinessSectorId",
                table: "Company",
                column: "BusinessSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_DefaultBankAccountCompanyId",
                table: "Company",
                column: "DefaultBankAccountCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDefaultBankAccount_BankAccountId",
                table: "CompanyDefaultBankAccount",
                column: "BankAccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDefaultTaxType_CompanyTaxTypeId",
                table: "CompanyDefaultTaxType",
                column: "CompanyTaxTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyImage_CompanyId",
                table: "CompanyImage",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLogo_ImageId",
                table: "CompanyLogo",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSettings_CountryCode",
                table: "CompanyRegionalSettings",
                column: "CountryCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSettings_CurrencyCode",
                table: "CompanyRegionalSettings",
                column: "CurrencyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSettings_DateFormatId",
                table: "CompanyRegionalSettings",
                column: "DateFormatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTaxType_CompanyId_TaxTypeId",
                table: "CompanyTaxType",
                columns: new[] { "CompanyId", "TaxTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTaxType_TaxTypeId",
                table: "CompanyTaxType",
                column: "TaxTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_CompanyId",
                table: "CompanyUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_UserId_CompanyId",
                table: "CompanyUser",
                columns: new[] { "UserId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_Name",
                table: "Country",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CategoryId",
                table: "Customer",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CompanyId_CategoryId",
                table: "Customer",
                columns: new[] { "CompanyId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_SalesPersonId",
                table: "Customer",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ShippingMethodId",
                table: "Customer",
                column: "ShippingMethodId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ShippingTermId",
                table: "Customer",
                column: "ShippingTermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccountsContact_CustomerContactId",
                table: "CustomerAccountsContact",
                column: "CustomerContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdjustment_CompanyId_CustomerId_JournalId",
                table: "CustomerAdjustment",
                columns: new[] { "CompanyId", "CustomerId", "JournalId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdjustment_CustomerId",
                table: "CustomerAdjustment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategory_CompanyId",
                table: "CustomerCategory",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategory_CustomerId",
                table: "CustomerCategory",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                table: "CustomerContact",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNote_NoteId",
                table: "CustomerNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWriteOff_CompanyId_CustomerId",
                table: "CustomerWriteOff",
                columns: new[] { "CompanyId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWriteOff_CompanyId_Number",
                table: "CustomerWriteOff",
                columns: new[] { "CompanyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DateFormat_Format",
                table: "DateFormat",
                column: "Format",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSetup_CompanyId_Id",
                table: "DocumentSetup",
                columns: new[] { "CompanyId", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSetup_PrintTemplateId",
                table: "DocumentSetup",
                column: "PrintTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CompanyId_Code",
                table: "Item",
                columns: new[] { "CompanyId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_TaxTypeId",
                table: "Item",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAdjustment_CompanyId_ItemId",
                table: "ItemAdjustment",
                columns: new[] { "CompanyId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemAdjustment_ItemId",
                table: "ItemAdjustment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_CompanyId",
                table: "ItemCategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategory_ParentId",
                table: "ItemCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_CompanyId",
                table: "Journal",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_CreditAccountId",
                table: "Journal",
                column: "CreditAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_DebitAccountId",
                table: "Journal",
                column: "DebitAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_TaxTypeId",
                table: "Journal",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalNote_NoteId",
                table: "JournalNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Note_CreatorId",
                table: "Note",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_Name",
                table: "PaymentMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBuyer_CompanyId_Id",
                table: "PurchaseBuyer",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBuyer_CompanyUserId",
                table: "PurchaseBuyer",
                column: "CompanyUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseBuyer_ContactId",
                table: "PurchaseBuyer",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_CompanyId_Number",
                table: "PurchaseDocument",
                columns: new[] { "CompanyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_LogoId",
                table: "PurchaseDocument",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_StatusId",
                table: "PurchaseDocument",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_SupplierDetailsId",
                table: "PurchaseDocument",
                column: "SupplierDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocumentLine_LineId",
                table: "PurchaseDocumentLine",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocumentNote_NoteId",
                table: "PurchaseDocumentNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocumentSupplierDetails_SupplierId",
                table: "PurchaseDocumentSupplierDetails",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoice_CompanyId_SupplierId",
                table: "PurchaseInvoice",
                columns: new[] { "CompanyId", "SupplierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoice_SupplierId",
                table: "PurchaseInvoice",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceReceipt_InvoiceDocumentId",
                table: "PurchaseInvoiceReceipt",
                column: "InvoiceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceReceipt_InvoiceId",
                table: "PurchaseInvoiceReceipt",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceReturn_ReturnId",
                table: "PurchaseInvoiceReturn",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLine_TaxTypeId",
                table: "PurchaseLine",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CompanyId",
                table: "PurchaseOrder",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_SupplierId_CompanyId",
                table: "PurchaseOrder",
                columns: new[] { "SupplierId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderInvoice_InvoiceId",
                table: "PurchaseOrderInvoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_BankAccountId",
                table: "PurchaseReceipt",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_CompanyId_SupplierId",
                table: "PurchaseReceipt",
                columns: new[] { "CompanyId", "SupplierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_LogoId",
                table: "PurchaseReceipt",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_PaymentMethodId",
                table: "PurchaseReceipt",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_SupplierDetailsId",
                table: "PurchaseReceipt",
                column: "SupplierDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_SupplierId",
                table: "PurchaseReceipt",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturn_CompanyId_SupplierId",
                table: "PurchaseReturn",
                columns: new[] { "CompanyId", "SupplierId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturn_SupplierId",
                table: "PurchaseReturn",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesCredit_CompanyId_CustomerId",
                table: "SalesCredit",
                columns: new[] { "CompanyId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesCredit_CustomerId",
                table: "SalesCredit",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CompanyId_Number",
                table: "SalesDocument",
                columns: new[] { "CompanyId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CreatorId",
                table: "SalesDocument",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CustomerDetailsId",
                table: "SalesDocument",
                column: "CustomerDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_LogoId",
                table: "SalesDocument",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_SalesPersonId",
                table: "SalesDocument",
                column: "SalesPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_ShippingMethodId",
                table: "SalesDocument",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_ShippingTermId",
                table: "SalesDocument",
                column: "ShippingTermId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_StatusId",
                table: "SalesDocument",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocumentCustomerDetails_CustomerId",
                table: "SalesDocumentCustomerDetails",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocumentLine_LineId",
                table: "SalesDocumentLine",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocumentNote_NoteId",
                table: "SalesDocumentNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CompanyId_CustomerId",
                table: "SalesInvoice",
                columns: new[] { "CompanyId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CustomerId",
                table: "SalesInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CustomerWriteOffId",
                table: "SalesInvoice",
                column: "CustomerWriteOffId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceCredit_CreditId",
                table: "SalesInvoiceCredit",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceReceipt_ReceiptId",
                table: "SalesInvoiceReceipt",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_ItemCode",
                table: "SalesLine",
                column: "ItemCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_TaxTypeId",
                table: "SalesLine",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CompanyId",
                table: "SalesOrder",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId_CompanyId",
                table: "SalesOrder",
                columns: new[] { "CustomerId", "CompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderInvoice_InvoiceId",
                table: "SalesOrderInvoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_CompanyId_ContactId",
                table: "SalesPerson",
                columns: new[] { "CompanyId", "ContactId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesPerson_CompanyUserId",
                table: "SalesPerson",
                column: "CompanyUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesQuote_CompanyId_CustomerId",
                table: "SalesQuote",
                columns: new[] { "CompanyId", "CustomerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesQuote_CustomerId",
                table: "SalesQuote",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesQuoteOrder_OrderId",
                table: "SalesQuoteOrder",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_BankAccountId",
                table: "SalesReceipt",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_CompanyId",
                table: "SalesReceipt",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_CustomerDetailsId",
                table: "SalesReceipt",
                column: "CustomerDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_CustomerId",
                table: "SalesReceipt",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_LogoId",
                table: "SalesReceipt",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_PaymentMethodId",
                table: "SalesReceipt",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethod_Name",
                table: "ShippingMethod",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingMethod_ShortName",
                table: "ShippingMethod",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingTerm_Name",
                table: "ShippingTerm",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingTerm_ShortName",
                table: "ShippingTerm",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CategoryId",
                table: "Supplier",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_CompanyId_VendorNumber",
                table: "Supplier",
                columns: new[] { "CompanyId", "VendorNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_TaxTypeId",
                table: "Supplier",
                column: "TaxTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountsContact_SupplierContactId",
                table: "SupplierAccountsContact",
                column: "SupplierContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAdjustment_CompanyId_JournalId",
                table: "SupplierAdjustment",
                columns: new[] { "CompanyId", "JournalId" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAdjustment_SupplierId",
                table: "SupplierAdjustment",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCategory_CompanyId",
                table: "SupplierCategory",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContact_SupplierId",
                table: "SupplierContact",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierNote_NoteId",
                table: "SupplierNote",
                column: "NoteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxType_Name_System_Group",
                table: "TaxType",
                columns: new[] { "Name", "System", "Group" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_CompanyTaxType_TaxTypeId",
                table: "Account",
                column: "TaxTypeId",
                principalTable: "CompanyTaxType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Company_CompanyId",
                table: "Account",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Company_CompanyId",
                table: "BankAccount",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_CompanyDefaultBankAccount_DefaultBankAccountCompanyId",
                table: "Company",
                column: "DefaultBankAccountCompanyId",
                principalTable: "CompanyDefaultBankAccount",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_CustomerCategory_CategoryId",
                table: "Customer",
                column: "CategoryId",
                principalTable: "CustomerCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Company_CompanyId",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyDefaultBankAccount_Company_CompanyId",
                table: "CompanyDefaultBankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Company_CompanyId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Company_CompanyId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesPerson_Company_CompanyId",
                table: "SalesPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_User_UserId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_CustomerCategory_CategoryId",
                table: "Customer");

            migrationBuilder.DropTable(
                name: "CompanyDefaultTaxType");

            migrationBuilder.DropTable(
                name: "CompanyLogo");

            migrationBuilder.DropTable(
                name: "CompanyMailSettings");

            migrationBuilder.DropTable(
                name: "CompanyRegionalSettings");

            migrationBuilder.DropTable(
                name: "CustomerAccountsContact");

            migrationBuilder.DropTable(
                name: "CustomerAdjustment");

            migrationBuilder.DropTable(
                name: "CustomerNote");

            migrationBuilder.DropTable(
                name: "CustomerSetup");

            migrationBuilder.DropTable(
                name: "DocumentSetup");

            migrationBuilder.DropTable(
                name: "ItemAdjustment");

            migrationBuilder.DropTable(
                name: "ItemInventory");

            migrationBuilder.DropTable(
                name: "ItemSetup");

            migrationBuilder.DropTable(
                name: "JournalNote");

            migrationBuilder.DropTable(
                name: "PaymentTerm");

            migrationBuilder.DropTable(
                name: "PurchaseBuyer");

            migrationBuilder.DropTable(
                name: "PurchaseDocumentLine");

            migrationBuilder.DropTable(
                name: "PurchaseDocumentNote");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceReceipt");

            migrationBuilder.DropTable(
                name: "PurchaseInvoiceReturn");

            migrationBuilder.DropTable(
                name: "PurchaseOrderInvoice");

            migrationBuilder.DropTable(
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "SalesDocumentLine");

            migrationBuilder.DropTable(
                name: "SalesDocumentNote");

            migrationBuilder.DropTable(
                name: "SalesInvoiceCredit");

            migrationBuilder.DropTable(
                name: "SalesInvoiceReceipt");

            migrationBuilder.DropTable(
                name: "SalesOrderInvoice");

            migrationBuilder.DropTable(
                name: "SalesQuoteOrder");

            migrationBuilder.DropTable(
                name: "SupplierAccountsContact");

            migrationBuilder.DropTable(
                name: "SupplierAdjustment");

            migrationBuilder.DropTable(
                name: "SupplierNote");

            migrationBuilder.DropTable(
                name: "SupplierSetup");

            migrationBuilder.DropTable(
                name: "SystemCompanyNumber");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "DateFormat");

            migrationBuilder.DropTable(
                name: "CustomerContact");

            migrationBuilder.DropTable(
                name: "DocumentPrintTemplate");

            migrationBuilder.DropTable(
                name: "PurchaseLine");

            migrationBuilder.DropTable(
                name: "PurchaseReceipt");

            migrationBuilder.DropTable(
                name: "PurchaseReturn");

            migrationBuilder.DropTable(
                name: "PurchaseInvoice");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "SalesLine");

            migrationBuilder.DropTable(
                name: "SalesCredit");

            migrationBuilder.DropTable(
                name: "SalesReceipt");

            migrationBuilder.DropTable(
                name: "SalesInvoice");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "SalesQuote");

            migrationBuilder.DropTable(
                name: "SupplierContact");

            migrationBuilder.DropTable(
                name: "Journal");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "PurchaseDocument");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "CustomerWriteOff");

            migrationBuilder.DropTable(
                name: "SalesDocument");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "PurchaseDocumentSupplierDetails");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "CompanyImage");

            migrationBuilder.DropTable(
                name: "DocumentStatus");

            migrationBuilder.DropTable(
                name: "SalesDocumentCustomerDetails");

            migrationBuilder.DropTable(
                name: "AccountCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "AccountCashFlowType");

            migrationBuilder.DropTable(
                name: "CompanyTaxType");

            migrationBuilder.DropTable(
                name: "SupplierCategory");

            migrationBuilder.DropTable(
                name: "TaxType");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "BusinessSector");

            migrationBuilder.DropTable(
                name: "CompanyDefaultBankAccount");

            migrationBuilder.DropTable(
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "BankAccountCategory");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "CustomerCategory");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropTable(
                name: "ShippingMethod");

            migrationBuilder.DropTable(
                name: "ShippingTerm");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "Contact");
        }
    }
}
