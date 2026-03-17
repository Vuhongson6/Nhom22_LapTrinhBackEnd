using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace museum_artifact_manager.Migrations
{
    /// <inheritdoc />
    public partial class MakeExhibitionIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Exhibitions_ExhibitionId",
                table: "Artifacts");

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Artifacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_Exhibitions_ExhibitionId",
                table: "Artifacts",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artifacts_Exhibitions_ExhibitionId",
                table: "Artifacts");

            migrationBuilder.AlterColumn<int>(
                name: "ExhibitionId",
                table: "Artifacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Artifacts_Exhibitions_ExhibitionId",
                table: "Artifacts",
                column: "ExhibitionId",
                principalTable: "Exhibitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
