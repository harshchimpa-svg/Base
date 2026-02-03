using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
//             migrationBuilder.CreateTable(
//                 name: "Organizations",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Logo = table.Column<string>(type: "text", nullable: true),
//                     TagLine = table.Column<string>(type: "text", nullable: true),
//                     WebSite = table.Column<string>(type: "text", nullable: true),
//                     PhoneNumber = table.Column<long>(type: "bigint", nullable: true),
//                     Email = table.Column<string>(type: "text", nullable: true),
//                     Facebook = table.Column<string>(type: "text", nullable: true),
//                     LinkedIn = table.Column<string>(type: "text", nullable: true),
//                     Instagram = table.Column<string>(type: "text", nullable: true),
//                     Description = table.Column<string>(type: "text", nullable: true),
//                     ParentId = table.Column<int>(type: "integer", nullable: true),
//                     IsApproved = table.Column<bool>(type: "boolean", nullable: false),
//                     ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: true),
//                     Discount = table.Column<decimal>(type: "numeric", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Organizations", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Organizations_Organizations_ParentId",
//                         column: x => x.ParentId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "About",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Profile = table.Column<string>(type: "text", nullable: false),
//                     SubTitel = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_About", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_About_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetRoles",
//                 columns: table => new
//                 {
//                     Id = table.Column<string>(type: "text", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: true),
//                     Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetRoles", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_AspNetRoles_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetUsers",
//                 columns: table => new
//                 {
//                     Id = table.Column<string>(type: "text", nullable: false),
//                     FirstName = table.Column<string>(type: "text", nullable: false),
//                     LastName = table.Column<string>(type: "text", nullable: true),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: true),
//                     AppToken = table.Column<string>(type: "text", nullable: true),
//                     SignalRToken = table.Column<string>(type: "text", nullable: true),
//                     UserType = table.Column<int>(type: "integer", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OtherDetails = table.Column<string>(type: "text", nullable: true),
//                     UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
//                     EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
//                     PasswordHash = table.Column<string>(type: "text", nullable: true),
//                     SecurityStamp = table.Column<string>(type: "text", nullable: true),
//                     ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
//                     PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
//                     TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
//                     LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
//                     LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
//                     AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetUsers", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_AspNetUsers_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Categori",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     ImageUrl = table.Column<string>(type: "text", nullable: false),
//                     Description = table.Column<string>(type: "text", nullable: false),
//                     ParentId = table.Column<int>(type: "integer", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Categori", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Categori_Categori_ParentId",
//                         column: x => x.ParentId,
//                         principalTable: "Categori",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Categori_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Contact",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false),
//                     Massage = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Contact", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Contact_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Customer",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: false),
//                     Notes = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Customer", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Customer_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "DietType",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_DietType", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_DietType_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Locations",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     LocationType = table.Column<int>(type: "integer", nullable: true),
//                     ShortName = table.Column<string>(type: "text", nullable: true),
//                     Code = table.Column<string>(type: "text", nullable: true),
//                     ParentId = table.Column<int>(type: "integer", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Locations", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Locations_Locations_ParentId",
//                         column: x => x.ParentId,
//                         principalTable: "Locations",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Locations_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Vendors",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: false),
//                     Profile = table.Column<string>(type: "text", nullable: false),
//                     Address = table.Column<string>(type: "text", nullable: false),
//                     Website = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Vendors", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Vendors_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetRoleClaims",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     RoleId = table.Column<string>(type: "text", nullable: false),
//                     ClaimType = table.Column<string>(type: "text", nullable: true),
//                     ClaimValue = table.Column<string>(type: "text", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
//                         column: x => x.RoleId,
//                         principalTable: "AspNetRoles",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Employee",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     LastName = table.Column<string>(type: "text", nullable: false),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: false),
//                     Alterphonenumber = table.Column<string>(type: "text", nullable: false),
//                     Address1 = table.Column<string>(type: "text", nullable: false),
//                     Address2 = table.Column<string>(type: "text", nullable: false),
//                     City = table.Column<string>(type: "text", nullable: false),
//                     State = table.Column<string>(type: "text", nullable: false),
//                     country = table.Column<string>(type: "text", nullable: false),
//                     RoleId = table.Column<string>(type: "text", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Employee", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Employee_AspNetRoles_RoleId",
//                         column: x => x.RoleId,
//                         principalTable: "AspNetRoles",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Employee_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetUserClaims",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     ClaimType = table.Column<string>(type: "text", nullable: true),
//                     ClaimValue = table.Column<string>(type: "text", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_AspNetUserClaims_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetUserLogins",
//                 columns: table => new
//                 {
//                     LoginProvider = table.Column<string>(type: "text", nullable: false),
//                     ProviderKey = table.Column<string>(type: "text", nullable: false),
//                     ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
//                     UserId = table.Column<string>(type: "text", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
//                     table.ForeignKey(
//                         name: "FK_AspNetUserLogins_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetUserRoles",
//                 columns: table => new
//                 {
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     RoleId = table.Column<string>(type: "text", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
//                     table.ForeignKey(
//                         name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
//                         column: x => x.RoleId,
//                         principalTable: "AspNetRoles",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_AspNetUserRoles_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "AspNetUserTokens",
//                 columns: table => new
//                 {
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     LoginProvider = table.Column<string>(type: "text", nullable: false),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Value = table.Column<string>(type: "text", nullable: true)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
//                     table.ForeignKey(
//                         name: "FK_AspNetUserTokens_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "OTPs",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Otp = table.Column<int>(type: "integer", nullable: false),
//                     IsChecked = table.Column<bool>(type: "boolean", nullable: false),
//                     ForOtp = table.Column<string>(type: "text", nullable: true),
//                     OtpSentOn = table.Column<int>(type: "integer", nullable: false),
//                     ForOtpType = table.Column<int>(type: "integer", nullable: false),
//                     TimesChecked = table.Column<int>(type: "integer", nullable: false),
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_OTPs", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_OTPs_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_OTPs_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Sale",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     UserId = table.Column<string>(type: "text", nullable: true),
//                     IsPaid = table.Column<bool>(type: "boolean", nullable: false),
//                     IsCanceld = table.Column<bool>(type: "boolean", nullable: false),
//                     InvoiceNo = table.Column<string>(type: "text", nullable: true),
//                     Discount = table.Column<decimal>(type: "numeric", nullable: false),
//                     NetAmount = table.Column<decimal>(type: "numeric", nullable: false),
//                     Tax = table.Column<int>(type: "integer", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Sale", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Sale_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Sale_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "UserAddress",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     Address1 = table.Column<string>(type: "text", nullable: true),
//                     Address2 = table.Column<string>(type: "text", nullable: true),
//                     City = table.Column<string>(type: "text", nullable: true),
//                     State = table.Column<string>(type: "text", nullable: true),
//                     Country = table.Column<string>(type: "text", nullable: true),
//                     PinCode = table.Column<int>(type: "integer", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_UserAddress", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_UserAddress_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_UserAddress_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "UserProfile",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     Weight = table.Column<decimal>(type: "numeric", nullable: false),
//                     Height = table.Column<decimal>(type: "numeric", nullable: false),
//                     UserLevelType = table.Column<int>(type: "integer", nullable: false),
//                     DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     Message = table.Column<string>(type: "text", nullable: false),
//                     PhoneNumber = table.Column<string>(type: "text", nullable: false),
//                     ProfileImageUrl = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_UserProfile", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_UserProfile_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_UserProfile_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Balance",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     CustomerId = table.Column<int>(type: "integer", nullable: true),
//                     BalanceType = table.Column<int>(type: "integer", nullable: false),
//                     Amount = table.Column<decimal>(type: "numeric", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Balance", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Balance_Customer_CustomerId",
//                         column: x => x.CustomerId,
//                         principalTable: "Customer",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Balance_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Diet",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     DietTypeId = table.Column<int>(type: "integer", nullable: true),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     Description = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Diet", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Diet_DietType_DietTypeId",
//                         column: x => x.DietTypeId,
//                         principalTable: "DietType",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Diet_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Exercise",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     DietTypeId = table.Column<int>(type: "integer", nullable: true),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Description = table.Column<string>(type: "text", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Exercise", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Exercise_DietType_DietTypeId",
//                         column: x => x.DietTypeId,
//                         principalTable: "DietType",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Exercise_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Gyms",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Price = table.Column<int>(type: "integer", nullable: false),
//                     Description = table.Column<string>(type: "text", nullable: false),
//                     LocationId = table.Column<int>(type: "integer", nullable: true),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Gyms", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Gyms_Locations_LocationId",
//                         column: x => x.LocationId,
//                         principalTable: "Locations",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Gyms_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Services",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     CatgoryId = table.Column<int>(type: "integer", nullable: true),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     SerialNo = table.Column<string>(type: "text", nullable: false),
//                     VendorId = table.Column<int>(type: "integer", nullable: true),
//                     Mesurment = table.Column<decimal>(type: "numeric", nullable: false),
//                     Price = table.Column<decimal>(type: "numeric", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Services", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Services_Categori_CatgoryId",
//                         column: x => x.CatgoryId,
//                         principalTable: "Categori",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_Services_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_Services_Vendors_VendorId",
//                         column: x => x.VendorId,
//                         principalTable: "Vendors",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "SalePayment",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     SaleId = table.Column<int>(type: "integer", nullable: true),
//                     MethodType = table.Column<int>(type: "integer", nullable: false),
//                     NetAmount = table.Column<decimal>(type: "numeric", nullable: false),
//                     PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     StatusType = table.Column<int>(type: "integer", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_SalePayment", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_SalePayment_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_SalePayment_Sale_SaleId",
//                         column: x => x.SaleId,
//                         principalTable: "Sale",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "SaleProduct",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     SaleId = table.Column<int>(type: "integer", nullable: true),
//                     Quantity = table.Column<int>(type: "integer", nullable: false),
//                     Price = table.Column<decimal>(type: "numeric", nullable: false),
//                     Discount = table.Column<decimal>(type: "numeric", nullable: false),
//                     taxe = table.Column<decimal>(type: "numeric", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_SaleProduct", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_SaleProduct_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_SaleProduct_Sale_SaleId",
//                         column: x => x.SaleId,
//                         principalTable: "Sale",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "PaymentLoge",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
//                     UserId = table.Column<string>(type: "text", nullable: true),
//                     Amount = table.Column<decimal>(type: "numeric", nullable: false),
//                     BalanceType = table.Column<int>(type: "integer", nullable: false),
//                     CustomerId = table.Column<int>(type: "integer", nullable: false),
//                     BalanceId = table.Column<int>(type: "integer", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_PaymentLoge", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_PaymentLoge_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_PaymentLoge_Balance_BalanceId",
//                         column: x => x.BalanceId,
//                         principalTable: "Balance",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_PaymentLoge_Customer_CustomerId",
//                         column: x => x.CustomerId,
//                         principalTable: "Customer",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_PaymentLoge_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "DietDocument",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     DietId = table.Column<int>(type: "integer", nullable: true),
//                     Document = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_DietDocument", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_DietDocument_Diet_DietId",
//                         column: x => x.DietId,
//                         principalTable: "Diet",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_DietDocument_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "ExerciseDocument",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     ExerciseId = table.Column<int>(type: "integer", nullable: true),
//                     Document = table.Column<string>(type: "text", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_ExerciseDocument", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_ExerciseDocument_Exercise_ExerciseId",
//                         column: x => x.ExerciseId,
//                         principalTable: "Exercise",
//                         principalColumn: "Id");
//                     table.ForeignKey(
//                         name: "FK_ExerciseDocument_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "GemTraner",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     UserId = table.Column<string>(type: "text", nullable: false),
//                     GymId = table.Column<int>(type: "integer", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_GemTraner", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_GemTraner_AspNetUsers_UserId",
//                         column: x => x.UserId,
//                         principalTable: "AspNetUsers",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_GemTraner_Gyms_GymId",
//                         column: x => x.GymId,
//                         principalTable: "Gyms",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_GemTraner_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                 });
//
//             migrationBuilder.CreateTable(
//                 name: "Clients",
//                 columns: table => new
//                 {
//                     Id = table.Column<int>(type: "integer", nullable: false)
//                         .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
//                     Name = table.Column<string>(type: "text", nullable: false),
//                     Email = table.Column<string>(type: "text", nullable: false),
//                     Phone = table.Column<string>(type: "text", nullable: false),
//                     ServiceId = table.Column<int>(type: "integer", nullable: true),
//                     Quantity = table.Column<decimal>(type: "numeric", nullable: false),
//                     CreatedBy = table.Column<string>(type: "text", nullable: true),
//                     UpdatedBy = table.Column<string>(type: "text", nullable: true),
//                     CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
//                     IsActive = table.Column<bool>(type: "boolean", nullable: false),
//                     IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
//                     OrganizationId = table.Column<int>(type: "integer", nullable: false)
//                 },
//                 constraints: table =>
//                 {
//                     table.PrimaryKey("PK_Clients", x => x.Id);
//                     table.ForeignKey(
//                         name: "FK_Clients_Organizations_OrganizationId",
//                         column: x => x.OrganizationId,
//                         principalTable: "Organizations",
//                         principalColumn: "Id",
//                         onDelete: ReferentialAction.Cascade);
//                     table.ForeignKey(
//                         name: "FK_Clients_Services_ServiceId",
//                         column: x => x.ServiceId,
//                         principalTable: "Services",
//                         principalColumn: "Id");
//                 });
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_About_OrganizationId",
//                 table: "About",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetRoleClaims_RoleId",
//                 table: "AspNetRoleClaims",
//                 column: "RoleId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetRoles_OrganizationId",
//                 table: "AspNetRoles",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "RoleNameIndex",
//                 table: "AspNetRoles",
//                 column: "NormalizedName");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetUserClaims_UserId",
//                 table: "AspNetUserClaims",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetUserLogins_UserId",
//                 table: "AspNetUserLogins",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetUserRoles_RoleId",
//                 table: "AspNetUserRoles",
//                 column: "RoleId");
//
//             migrationBuilder.CreateIndex(
//                 name: "EmailIndex",
//                 table: "AspNetUsers",
//                 column: "NormalizedEmail");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_AspNetUsers_OrganizationId",
//                 table: "AspNetUsers",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "UserNameIndex",
//                 table: "AspNetUsers",
//                 column: "NormalizedUserName",
//                 unique: true);
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Balance_CustomerId",
//                 table: "Balance",
//                 column: "CustomerId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Balance_OrganizationId",
//                 table: "Balance",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Categori_OrganizationId",
//                 table: "Categori",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Categori_ParentId",
//                 table: "Categori",
//                 column: "ParentId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Clients_OrganizationId",
//                 table: "Clients",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Clients_ServiceId",
//                 table: "Clients",
//                 column: "ServiceId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Contact_OrganizationId",
//                 table: "Contact",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Customer_OrganizationId",
//                 table: "Customer",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Diet_DietTypeId",
//                 table: "Diet",
//                 column: "DietTypeId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Diet_OrganizationId",
//                 table: "Diet",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_DietDocument_DietId",
//                 table: "DietDocument",
//                 column: "DietId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_DietDocument_OrganizationId",
//                 table: "DietDocument",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_DietType_OrganizationId",
//                 table: "DietType",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Employee_OrganizationId",
//                 table: "Employee",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Employee_RoleId",
//                 table: "Employee",
//                 column: "RoleId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Exercise_DietTypeId",
//                 table: "Exercise",
//                 column: "DietTypeId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Exercise_OrganizationId",
//                 table: "Exercise",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_ExerciseDocument_ExerciseId",
//                 table: "ExerciseDocument",
//                 column: "ExerciseId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_ExerciseDocument_OrganizationId",
//                 table: "ExerciseDocument",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_GemTraner_GymId",
//                 table: "GemTraner",
//                 column: "GymId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_GemTraner_OrganizationId",
//                 table: "GemTraner",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_GemTraner_UserId",
//                 table: "GemTraner",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Gyms_LocationId",
//                 table: "Gyms",
//                 column: "LocationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Gyms_OrganizationId",
//                 table: "Gyms",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Locations_OrganizationId",
//                 table: "Locations",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Locations_ParentId",
//                 table: "Locations",
//                 column: "ParentId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Organizations_ParentId",
//                 table: "Organizations",
//                 column: "ParentId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_OTPs_OrganizationId",
//                 table: "OTPs",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_OTPs_UserId",
//                 table: "OTPs",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_PaymentLoge_BalanceId",
//                 table: "PaymentLoge",
//                 column: "BalanceId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_PaymentLoge_CustomerId",
//                 table: "PaymentLoge",
//                 column: "CustomerId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_PaymentLoge_OrganizationId",
//                 table: "PaymentLoge",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_PaymentLoge_UserId",
//                 table: "PaymentLoge",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Sale_OrganizationId",
//                 table: "Sale",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Sale_UserId",
//                 table: "Sale",
//                 column: "UserId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_SalePayment_OrganizationId",
//                 table: "SalePayment",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_SalePayment_SaleId",
//                 table: "SalePayment",
//                 column: "SaleId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_SaleProduct_OrganizationId",
//                 table: "SaleProduct",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_SaleProduct_SaleId",
//                 table: "SaleProduct",
//                 column: "SaleId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Services_CatgoryId",
//                 table: "Services",
//                 column: "CatgoryId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Services_OrganizationId",
//                 table: "Services",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Services_VendorId",
//                 table: "Services",
//                 column: "VendorId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_UserAddress_OrganizationId",
//                 table: "UserAddress",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_UserAddress_UserId",
//                 table: "UserAddress",
//                 column: "UserId",
//                 unique: true);
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_UserProfile_OrganizationId",
//                 table: "UserProfile",
//                 column: "OrganizationId");
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_UserProfile_UserId",
//                 table: "UserProfile",
//                 column: "UserId",
//                 unique: true);
//
//             migrationBuilder.CreateIndex(
//                 name: "IX_Vendors_OrganizationId",
//                 table: "Vendors",
//                 column: "OrganizationId");
         }
//
//         /// <inheritdoc />
         protected override void Down(MigrationBuilder migrationBuilder)
         {
//             migrationBuilder.DropTable(
//                 name: "About");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetRoleClaims");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetUserClaims");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetUserLogins");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetUserRoles");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetUserTokens");
//
//             migrationBuilder.DropTable(
//                 name: "Clients");
//
//             migrationBuilder.DropTable(
//                 name: "Contact");
//
//             migrationBuilder.DropTable(
//                 name: "DietDocument");
//
//             migrationBuilder.DropTable(
//                 name: "Employee");
//
//             migrationBuilder.DropTable(
//                 name: "ExerciseDocument");
//
//             migrationBuilder.DropTable(
//                 name: "GemTraner");
//
//             migrationBuilder.DropTable(
//                 name: "OTPs");
//
//             migrationBuilder.DropTable(
//                 name: "PaymentLoge");
//
//             migrationBuilder.DropTable(
//                 name: "SalePayment");
//
//             migrationBuilder.DropTable(
//                 name: "SaleProduct");
//
//             migrationBuilder.DropTable(
//                 name: "UserAddress");
//
//             migrationBuilder.DropTable(
//                 name: "UserProfile");
//
//             migrationBuilder.DropTable(
//                 name: "Services");
//
//             migrationBuilder.DropTable(
//                 name: "Diet");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetRoles");
//
//             migrationBuilder.DropTable(
//                 name: "Exercise");
//
//             migrationBuilder.DropTable(
//                 name: "Gyms");
//
//             migrationBuilder.DropTable(
//                 name: "Balance");
//
//             migrationBuilder.DropTable(
//                 name: "Sale");
//
//             migrationBuilder.DropTable(
//                 name: "Categori");
//
//             migrationBuilder.DropTable(
//                 name: "Vendors");
//
//             migrationBuilder.DropTable(
//                 name: "DietType");
//
//             migrationBuilder.DropTable(
//                 name: "Locations");
//
//             migrationBuilder.DropTable(
//                 name: "Customer");
//
//             migrationBuilder.DropTable(
//                 name: "AspNetUsers");
//
//             migrationBuilder.DropTable(
//                 name: "Organizations");
        }
    }
}
