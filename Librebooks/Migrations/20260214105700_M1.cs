using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Librebooks.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountCashFlowType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCashFlowType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessSector", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanySetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prefix = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    NextNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberFormat = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 75, nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    Mobile = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    DialingCode = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWriteOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Format = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentPrintTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DocumentType = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 6, nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShippingTerm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    ShortName = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    System = table.Column<bool>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Birthday = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Photo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    DateRegistered = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DateLastLoggedIn = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LoginHash = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    RefreshSecurityStamp = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    RefreshLoginHash = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
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
                name: "VerificationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    HashString = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime", nullable: false),
                    Attempts = table.Column<short>(type: "INTEGER", nullable: false),
                    MaxAttemptsAllowed = table.Column<short>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ClassType = table.Column<string>(type: "TEXT", maxLength: 75, nullable: true),
                    CashFlowTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UniqueNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    LegalName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TradingName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    RegNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    VATNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    PostalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    PhysicalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    FaxNumber = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    YearsInBusiness = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessSectorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_BusinessSector_BusinessSectorId",
                        column: x => x.BusinessSectorId,
                        principalTable: "BusinessSector",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Actionable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BankName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AccountNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    BranchName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    BranchCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    SwiftCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                        name: "FK_BankAccount_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
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
                name: "CompanyImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "CompanyMailSetup",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SmtpServerName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    SmtpPort = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMailSetup", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyMailSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRegionalSetup",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    DecimalMark = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    ThousandsSeperator = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    DateFormatId = table.Column<int>(type: "INTEGER", nullable: false),
                    CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundToNearest = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRegionalSetup", x => x.CompanyId);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSetup_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSetup_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSetup_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyRegionalSetup_DateFormat_DateFormatId",
                        column: x => x.DateFormatId,
                        principalTable: "DateFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTaxType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTaxType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyTaxType_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "CustomerCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSetup",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    NextNumber = table.Column<int>(type: "INTEGER", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", maxLength: 75, nullable: true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Prefix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    NextNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LeadingZeros = table.Column<short>(type: "INTEGER", nullable: false),
                    FooterMessage = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    NoteMessage = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    PrintTemplateId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    NextNumber = table.Column<int>(type: "INTEGER", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierCategory_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierSetup",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prefix = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    NextNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberFormat = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefaultBankAccount",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "CompanyLogo",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "DocumentCompanyInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    PhysicalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    PostalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    LogoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCompanyInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCompanyInfo_CompanyImage_LogoId",
                        column: x => x.LogoId,
                        principalTable: "CompanyImage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentCompanyInfo_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentCompanyInfo_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 75, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ParentAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    System = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Account_CompanyTaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "CompanyTaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDefaultTaxType",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyTaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseBuyer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyUserId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyUserId = table.Column<int>(type: "INTEGER", nullable: true)
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Physical = table.Column<bool>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<int>(type: "INTEGER", nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegisteredName = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    TradingName = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    VendorNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    VATRegNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Fax = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    PhysicalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    PostalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PaymentTermId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "Journal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    datetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 75, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    DebitAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Posted = table.Column<bool>(type: "INTEGER", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Journal_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LegalName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    TradingName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DeliveryAddress = table.Column<string>(type: "TEXT", nullable: true),
                    BillingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 75, nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    PaymentTermInDays = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentTermId = table.Column<int>(type: "INTEGER", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesPersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    AcceptsElectronicInvoices = table.Column<bool>(type: "INTEGER", nullable: false),
                    ShippingTermId = table.Column<int>(type: "INTEGER", nullable: false),
                    ShippingMethodId = table.Column<int>(type: "INTEGER", nullable: false),
                    Website = table.Column<string>(type: "TEXT", nullable: true),
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
                        name: "FK_Customer_CustomerCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CustomerCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Customer_PaymentTerm_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerm",
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
                name: "ItemAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldQuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    OldPrice = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "ItemInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemInfo_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemInventory",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    QuantityOnHand = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    MinQuantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    MaxQuantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
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
                name: "ItemPriceAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    OldPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    NewPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPriceAdjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPriceAdjustment_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsItemType = table.Column<bool>(type: "INTEGER", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    ItemCode = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_PurchaseLine_Item_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Item",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesLine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsItemType = table.Column<bool>(type: "INTEGER", nullable: false),
                    ItemCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DiscountRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: true),
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
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesLine_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DocumentSupplierInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierName = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    PhysicalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    PostalAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: true),
                    VATNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSupplierInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentSupplierInfo_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReceipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Reconciled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_PurchaseReceipt_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierContact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "JournalNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    JournalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalNote", x => x.NoteId);
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
                name: "SupplierAdjustment",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "CustomerAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    TaxTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxRate = table.Column<int>(type: "decimal(5,2)", nullable: false),
                    ExclusiveAmount = table.Column<decimal>(type: "decimal(15,2)", nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAdjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAdjustment_TaxType_TaxTypeId",
                        column: x => x.TaxTypeId,
                        principalTable: "TaxType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerNote",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerNote", x => x.NoteId);
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
                name: "DocumentCustomerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BillingAddress = table.Column<string>(type: "TEXT", maxLength: 155, nullable: false),
                    ShippingAddress = table.Column<string>(type: "TEXT", maxLength: 105, nullable: false),
                    VATNumber = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCustomerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCustomerInfo_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesReceipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Comments = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Archived = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reconciled = table.Column<bool>(type: "INTEGER", nullable: false),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    BankAccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "INTEGER", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SuppplierReference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Message = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Footer = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Currency = table.Column<string>(type: "TEXT", nullable: true),
                    Printed = table.Column<bool>(type: "INTEGER", nullable: false),
                    SupplierInfoId = table.Column<int>(type: "INTEGER", nullable: true),
                    CompanyInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_DocumentCompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "DocumentCompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_DocumentStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DocumentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseDocument_DocumentSupplierInfo_SupplierInfoId",
                        column: x => x.SupplierInfoId,
                        principalTable: "DocumentSupplierInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierAccountsContact",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierContactContactId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierAccountsContact", x => new { x.SupplierId, x.SupplierContactId });
                    table.ForeignKey(
                        name: "FK_SupplierAccountsContact_SupplierContact_SupplierContactContactId",
                        column: x => x.SupplierContactContactId,
                        principalTable: "SupplierContact",
                        principalColumn: "ContactId");
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
                name: "CustomerAccountsContact",
                columns: table => new
                {
                    CustomerContactId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccountsContact", x => x.CustomerContactId);
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Number = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CustomerReference = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CustomerInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyInfoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    FooterMessage = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    TaxExempt = table.Column<bool>(type: "INTEGER", nullable: false),
                    CurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SalesPersonId = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingTermId = table.Column<int>(type: "INTEGER", nullable: true),
                    ShippingMethodId = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Recorded = table.Column<bool>(type: "INTEGER", nullable: false),
                    StatusId = table.Column<int>(type: "INTEGER", nullable: false),
                    Printed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalesPersonContactId = table.Column<int>(type: "INTEGER", nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesDocument_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_DocumentCompanyInfo_CompanyInfoId",
                        column: x => x.CompanyInfoId,
                        principalTable: "DocumentCompanyInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_DocumentCustomerInfo_CustomerInfoId",
                        column: x => x.CustomerInfoId,
                        principalTable: "DocumentCustomerInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_DocumentStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "DocumentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_SalesPerson_SalesPersonContactId",
                        column: x => x.SalesPersonContactId,
                        principalTable: "SalesPerson",
                        principalColumn: "ContactId");
                    table.ForeignKey(
                        name: "FK_SalesDocument_ShippingMethod_ShippingMethodId",
                        column: x => x.ShippingMethodId,
                        principalTable: "ShippingMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_ShippingTerm_ShippingTermId",
                        column: x => x.ShippingTermId,
                        principalTable: "ShippingTerm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesDocument_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDocumentLine",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    LineId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDocumentNote", x => x.NoteId);
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
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoice", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseInvoice_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequestForQuote",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequestForQuote", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseRequestForQuote_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturn",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseReturn", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_PurchaseDocument_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "PurchaseDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesCredit",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesCredit", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_SalesCredit_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
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
                    LineId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(15,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocumentLine", x => x.LineId);
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
                    NoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDocumentNote", x => x.NoteId);
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
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    DocumentId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "PurchaseInvoiceReceipt",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInvoiceReceipt", x => new { x.ReceiptId, x.InvoiceId });
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
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    ReturnId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
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
                name: "SalesInvoiceCredit",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreditId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiptId = table.Column<int>(type: "INTEGER", nullable: false),
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
                name: "SalesInvoiceWriteoff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    WriteOffId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    RowVersion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceWriteoff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceWriteoff_CustomerWriteOff_WriteOffId",
                        column: x => x.WriteOffId,
                        principalTable: "CustomerWriteOff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceWriteoff_SalesInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "DocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderInvoice",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    InvoiceId = table.Column<int>(type: "INTEGER", nullable: false)
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
                    QuoteId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
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
                name: "IX_BankAccount_CompanyId_Id",
                table: "BankAccount",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_PaymentMethodId",
                table: "BankAccount",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSector_Name",
                table: "BusinessSector",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_BusinessSectorId",
                table: "Company",
                column: "BusinessSectorId");

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
                name: "IX_CompanyImage_CompanyId_Id",
                table: "CompanyImage",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLogo_ImageId",
                table: "CompanyLogo",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSetup_CountryId",
                table: "CompanyRegionalSetup",
                column: "CountryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSetup_CurrencyId",
                table: "CompanyRegionalSetup",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegionalSetup_DateFormatId",
                table: "CompanyRegionalSetup",
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
                name: "IX_Currency_Name",
                table: "Currency",
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
                name: "IX_Customer_PaymentTermId",
                table: "Customer",
                column: "PaymentTermId",
                unique: true);

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
                name: "IX_CustomerAccountsContact_CustomerId_CustomerContactId",
                table: "CustomerAccountsContact",
                columns: new[] { "CustomerId", "CustomerContactId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdjustment_CompanyId_CustomerId_Id",
                table: "CustomerAdjustment",
                columns: new[] { "CompanyId", "CustomerId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdjustment_CustomerId",
                table: "CustomerAdjustment",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAdjustment_TaxTypeId",
                table: "CustomerAdjustment",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerCategory_CompanyId_Id",
                table: "CustomerCategory",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                table: "CustomerContact",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId_ContactId",
                table: "CustomerContact",
                columns: new[] { "CustomerId", "ContactId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerNote_CustomerId_NoteId",
                table: "CustomerNote",
                columns: new[] { "CustomerId", "NoteId" });

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
                name: "IX_DocumentCompanyInfo_CompanyId",
                table: "DocumentCompanyInfo",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCompanyInfo_CompanyId_Id",
                table: "DocumentCompanyInfo",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCompanyInfo_CurrencyId",
                table: "DocumentCompanyInfo",
                column: "CurrencyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCompanyInfo_LogoId",
                table: "DocumentCompanyInfo",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCustomerInfo_CustomerId",
                table: "DocumentCustomerInfo",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSetup_CompanyId",
                table: "DocumentSetup",
                column: "CompanyId",
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
                name: "IX_DocumentSupplierInfo_SupplierId",
                table: "DocumentSupplierInfo",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Code",
                table: "Item",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_CompanyId_Id",
                table: "Item",
                columns: new[] { "CompanyId", "Id" },
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
                name: "IX_ItemInfo_ItemId",
                table: "ItemInfo",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPriceAdjustment_ItemId",
                table: "ItemPriceAdjustment",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_CompanyId_Id",
                table: "Journal",
                columns: new[] { "CompanyId", "Id" });

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
                name: "IX_JournalNote_JournalId_NoteId",
                table: "JournalNote",
                columns: new[] { "JournalId", "NoteId" });

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
                name: "IX_PaymentTerm_Name",
                table: "PaymentTerm",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerm_ShortName",
                table: "PaymentTerm",
                column: "ShortName",
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
                name: "IX_PurchaseDocument_CompanyInfoId",
                table: "PurchaseDocument",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_StatusId",
                table: "PurchaseDocument",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocument_SupplierInfoId",
                table: "PurchaseDocument",
                column: "SupplierInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocumentLine_LineId",
                table: "PurchaseDocumentLine",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDocumentNote_DocumentId",
                table: "PurchaseDocumentNote",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoice_CompanyId_SupplierId_DocumentId",
                table: "PurchaseInvoice",
                columns: new[] { "CompanyId", "SupplierId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceReceipt_InvoiceId",
                table: "PurchaseInvoiceReceipt",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseInvoiceReturn_ReturnId",
                table: "PurchaseInvoiceReturn",
                column: "ReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLine_ItemCode",
                table: "PurchaseLine",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseLine_TaxTypeId",
                table: "PurchaseLine",
                column: "TaxTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_CompanyId",
                table: "PurchaseOrder",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_SupplierId_CompanyId_DocumentId",
                table: "PurchaseOrder",
                columns: new[] { "SupplierId", "CompanyId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderInvoice_InvoiceId",
                table: "PurchaseOrderInvoice",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_BankAccountId",
                table: "PurchaseReceipt",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_CompanyId",
                table: "PurchaseReceipt",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_PaymentMethodId",
                table: "PurchaseReceipt",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReceipt_SupplierId",
                table: "PurchaseReceipt",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturn_CompanyId_SupplierId_DocumentId",
                table: "PurchaseReturn",
                columns: new[] { "CompanyId", "SupplierId", "DocumentId" });

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
                name: "IX_SalesCredit_CompanyId_CustomerId_DocumentId",
                table: "SalesCredit",
                columns: new[] { "CompanyId", "CustomerId", "DocumentId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CompanyInfoId",
                table: "SalesDocument",
                column: "CompanyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CreatorId",
                table: "SalesDocument",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CurrencyId",
                table: "SalesDocument",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_CustomerInfoId",
                table: "SalesDocument",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocument_SalesPersonContactId",
                table: "SalesDocument",
                column: "SalesPersonContactId");

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
                name: "IX_SalesDocumentLine_DocumentId",
                table: "SalesDocumentLine",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesDocumentNote_DocumentId",
                table: "SalesDocumentNote",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CompanyId_CustomerId_DocumentId",
                table: "SalesInvoice",
                columns: new[] { "CompanyId", "CustomerId", "DocumentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_CustomerId",
                table: "SalesInvoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceCredit_CreditId",
                table: "SalesInvoiceCredit",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceReceipt_ReceiptId",
                table: "SalesInvoiceReceipt",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceWriteoff_InvoiceId_WriteOffId",
                table: "SalesInvoiceWriteoff",
                columns: new[] { "InvoiceId", "WriteOffId" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceWriteoff_WriteOffId",
                table: "SalesInvoiceWriteoff",
                column: "WriteOffId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_ItemCode",
                table: "SalesLine",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_SalesLine_ItemId",
                table: "SalesLine",
                column: "ItemId");

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
                column: "InvoiceId",
                unique: true);

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
                name: "IX_SalesQuote_CompanyId_CustomerId_DocumentId",
                table: "SalesQuote",
                columns: new[] { "CompanyId", "CustomerId", "DocumentId" });

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
                name: "IX_SalesReceipt_CompanyId_Id",
                table: "SalesReceipt",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReceipt_CustomerId",
                table: "SalesReceipt",
                column: "CustomerId");

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
                name: "IX_Supplier_CompanyId_Id",
                table: "Supplier",
                columns: new[] { "CompanyId", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_TaxTypeId",
                table: "Supplier",
                column: "TaxTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountsContact_SupplierContactContactId",
                table: "SupplierAccountsContact",
                column: "SupplierContactContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAccountsContact_SupplierContactId",
                table: "SupplierAccountsContact",
                column: "SupplierContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAdjustment_CompanyId_SupplierId_JournalId",
                table: "SupplierAdjustment",
                columns: new[] { "CompanyId", "SupplierId", "JournalId" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplierAdjustment_SupplierId",
                table: "SupplierAdjustment",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierCategory_CompanyId_Id",
                table: "SupplierCategory",
                columns: new[] { "CompanyId", "Id" });

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
                name: "IX_TaxType_Name",
                table: "TaxType",
                column: "Name",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDefaultBankAccount");

            migrationBuilder.DropTable(
                name: "CompanyDefaultTaxType");

            migrationBuilder.DropTable(
                name: "CompanyLogo");

            migrationBuilder.DropTable(
                name: "CompanyMailSetup");

            migrationBuilder.DropTable(
                name: "CompanyRegionalSetup");

            migrationBuilder.DropTable(
                name: "CompanySetup");

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
                name: "ItemInfo");

            migrationBuilder.DropTable(
                name: "ItemInventory");

            migrationBuilder.DropTable(
                name: "ItemPriceAdjustment");

            migrationBuilder.DropTable(
                name: "ItemSetup");

            migrationBuilder.DropTable(
                name: "JournalNote");

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
                name: "PurchaseRequestForQuote");

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
                name: "SalesInvoiceWriteoff");

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
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "VerificationRequest");

            migrationBuilder.DropTable(
                name: "Country");

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
                name: "CustomerWriteOff");

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
                name: "BankAccount");

            migrationBuilder.DropTable(
                name: "SalesDocument");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "DocumentSupplierInfo");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "BankAccountCategory");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "DocumentCompanyInfo");

            migrationBuilder.DropTable(
                name: "DocumentCustomerInfo");

            migrationBuilder.DropTable(
                name: "DocumentStatus");

            migrationBuilder.DropTable(
                name: "AccountCategory");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "CompanyImage");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AccountCashFlowType");

            migrationBuilder.DropTable(
                name: "CompanyTaxType");

            migrationBuilder.DropTable(
                name: "SupplierCategory");

            migrationBuilder.DropTable(
                name: "CustomerCategory");

            migrationBuilder.DropTable(
                name: "PaymentTerm");

            migrationBuilder.DropTable(
                name: "SalesPerson");

            migrationBuilder.DropTable(
                name: "ShippingMethod");

            migrationBuilder.DropTable(
                name: "ShippingTerm");

            migrationBuilder.DropTable(
                name: "TaxType");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BusinessSector");
        }
    }
}
