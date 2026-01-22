using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "InstagramId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "LinkedInId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "UserProfile");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "UserProfile",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "UserProfile",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "UserProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserRoleType",
                table: "UserProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                table: "UserProfile",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "UserProfile",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "UserRoleType",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "message",
                table: "UserProfile");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DOB",
                table: "UserProfile",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FacebookId",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "UserProfile",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramId",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInId",
                table: "UserProfile",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "UserProfile",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "UserProfile",
                type: "text",
                nullable: true);
        }
    }
}
