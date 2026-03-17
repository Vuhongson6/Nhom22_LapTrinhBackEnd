using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace museum_artifact_manager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exhibitions",
                columns: static table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: static table =>
                {
                    table.PrimaryKey("PK_Exhibitions", static x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: static table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: static table =>
                {
                    table.PrimaryKey("PK_Staffs", static x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Artifacts",
                columns: static table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExhibitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: static table =>
                {
                    table.PrimaryKey("PK_Artifacts", static x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artifacts_Exhibitions_ExhibitionId",
                        column: static x => x.ExhibitionId,
                        principalTable: "Exhibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restorations",
                columns: static table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtifactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: static table =>
                {
                    table.PrimaryKey("PK_Restorations", static x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restorations_Artifacts_ArtifactId",
                        column: static x => x.ArtifactId,
                        principalTable: "Artifacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artifacts_ExhibitionId",
                table: "Artifacts",
                column: "ExhibitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Restorations_ArtifactId",
                table: "Restorations",
                column: "ArtifactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restorations");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Artifacts");

            migrationBuilder.DropTable(
                name: "Exhibitions");
        }
    }
}
