using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreAngular.Migrations
{
    public partial class CreateIdentitySchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblServers",
                columns: table => new
                {
                    sqlServerPath = table.Column<string>(maxLength: 200, nullable: false),
                    isEnabled = table.Column<bool>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblServers", x => x.sqlServerPath);
                });

            migrationBuilder.CreateTable(
                name: "tblJobs",
                columns: table => new
                {
                    guid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    lastRunDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    lastRunOutcome = table.Column<int>(nullable: true),
                    lastOutcomeMessage = table.Column<string>(nullable: true),
                    nextRunDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    description = table.Column<string>(nullable: true),
                    jobOwner = table.Column<string>(maxLength: 200, nullable: true),
                    jobCategory = table.Column<string>(maxLength: 200, nullable: true),
                    numberOfSteps = table.Column<int>(nullable: false),
                    jobEnabled = table.Column<bool>(nullable: false),
                    isScheduled = table.Column<bool>(nullable: false),
                    sheduleName = table.Column<string>(maxLength: 200, nullable: true),
                    frequency = table.Column<string>(maxLength: 100, nullable: true),
                    recurrence = table.Column<string>(maxLength: 100, nullable: true),
                    subdayFrequency = table.Column<string>(maxLength: 100, nullable: true),
                    MaxDuration = table.Column<long>(nullable: true),
                    LastRunDuration = table.Column<long>(nullable: true),
                    lastRunStepNumber = table.Column<int>(nullable: true),
                    lastRunStepName = table.Column<string>(maxLength: 200, nullable: false),
                    lastRunStepMessage = table.Column<string>(nullable: false),
                    lastRunCommand = table.Column<string>(nullable: false),
                    isRunning = table.Column<bool>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    sqlServerPath = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblJobs__497F6CB4915B0B62", x => x.guid);
                    table.ForeignKey(
                        name: "FK_tblJobs_tblServers",
                        column: x => x.sqlServerPath,
                        principalTable: "tblServers",
                        principalColumn: "sqlServerPath",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblJobs_sqlServerPath",
                table: "tblJobs",
                column: "sqlServerPath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblJobs");

            migrationBuilder.DropTable(
                name: "tblServers");
        }
    }
}
