using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImprovByExample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoGenerationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Platform",
                table: "VideoReferences",
                newName: "VideoPlatformId");

            migrationBuilder.RenameColumn(
                name: "SourceType",
                table: "ActivitySources",
                newName: "SourceTypeId");

            migrationBuilder.CreateTable(
                name: "SourceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    UpdatedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false),
                    UpdatedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoPlatforms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideoReferences_VideoPlatformId",
                table: "VideoReferences",
                column: "VideoPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySources_SourceTypeId",
                table: "ActivitySources",
                column: "SourceTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivitySources_SourceTypes_SourceTypeId",
                table: "ActivitySources",
                column: "SourceTypeId",
                principalTable: "SourceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoReferences_VideoPlatforms_VideoPlatformId",
                table: "VideoReferences",
                column: "VideoPlatformId",
                principalTable: "VideoPlatforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivitySources_SourceTypes_SourceTypeId",
                table: "ActivitySources");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoReferences_VideoPlatforms_VideoPlatformId",
                table: "VideoReferences");

            migrationBuilder.DropTable(
                name: "SourceTypes");

            migrationBuilder.DropTable(
                name: "VideoPlatforms");

            migrationBuilder.DropIndex(
                name: "IX_VideoReferences_VideoPlatformId",
                table: "VideoReferences");

            migrationBuilder.DropIndex(
                name: "IX_ActivitySources_SourceTypeId",
                table: "ActivitySources");

            migrationBuilder.RenameColumn(
                name: "VideoPlatformId",
                table: "VideoReferences",
                newName: "Platform");

            migrationBuilder.RenameColumn(
                name: "SourceTypeId",
                table: "ActivitySources",
                newName: "SourceType");
        }
    }
}
