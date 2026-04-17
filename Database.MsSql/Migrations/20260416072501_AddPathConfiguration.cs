using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.MsSql.Migrations
{
    /// <inheritdoc />
    public partial class AddPathConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OPTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PathCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocalPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileSearchOption = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BackupPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ErrorPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RemotePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RemoteServer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RemotePort = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    RemoteUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RemotePassword = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPTS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OPTS_Active",
                table: "OPTS",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_OPTS_PathCode",
                table: "OPTS",
                column: "PathCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OPTS");
        }
    }
}
