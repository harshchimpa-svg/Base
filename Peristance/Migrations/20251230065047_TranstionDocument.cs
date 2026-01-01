using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TranstionDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TranstionDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransicstionId = table.Column<int>(type: "integer", nullable: false),
                    CatgoryId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    OrganizationId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranstionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranstionDocument_Catgory_CatgoryId",
                        column: x => x.CatgoryId,
                        principalTable: "Catgory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TranstionDocument_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TranstionDocument_Transicstions_TransicstionId",
                        column: x => x.TransicstionId,
                        principalTable: "Transicstions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TranstionDocument_CatgoryId",
                table: "TranstionDocument",
                column: "CatgoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TranstionDocument_OrganizationId",
                table: "TranstionDocument",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_TranstionDocument_TransicstionId",
                table: "TranstionDocument",
                column: "TransicstionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranstionDocument");
        }
    }
}
