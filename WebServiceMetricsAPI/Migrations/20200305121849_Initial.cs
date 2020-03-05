using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebServiceMetricsAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetricsRuns",
                columns: table => new
                {
                    MetricRunId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestUrl = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    NumberOfRequestsToSend = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricsRuns", x => x.MetricRunId);
                });

            migrationBuilder.CreateTable(
                name: "MetricsResults",
                columns: table => new
                {
                    MetricsResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MetricsRunId = table.Column<int>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    TimeElapsedInMilliseconds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetricsResults", x => x.MetricsResultId);
                    table.ForeignKey(
                        name: "FK_MetricsResults_MetricsRuns_MetricsRunId",
                        column: x => x.MetricsRunId,
                        principalTable: "MetricsRuns",
                        principalColumn: "MetricRunId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetricsResults_MetricsRunId",
                table: "MetricsResults",
                column: "MetricsRunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetricsResults");

            migrationBuilder.DropTable(
                name: "MetricsRuns");
        }
    }
}
