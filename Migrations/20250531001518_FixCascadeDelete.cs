using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_Lignes_LigneId",
                table: "Postes");

            migrationBuilder.AlterColumn<int>(
                name: "LigneId",
                table: "Postes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Lignes_LigneId",
                table: "Postes",
                column: "LigneId",
                principalTable: "Lignes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postes_Lignes_LigneId",
                table: "Postes");

            migrationBuilder.AlterColumn<int>(
                name: "LigneId",
                table: "Postes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Postes_Lignes_LigneId",
                table: "Postes",
                column: "LigneId",
                principalTable: "Lignes",
                principalColumn: "Id");
        }
    }
}
