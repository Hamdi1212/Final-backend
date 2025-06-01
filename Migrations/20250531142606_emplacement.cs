using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class emplacement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentEmplacement",
                table: "Equipements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LigneId",
                table: "Equipements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PosteId",
                table: "Equipements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjetId",
                table: "Equipements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Equipements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_LigneId",
                table: "Equipements",
                column: "LigneId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_PosteId",
                table: "Equipements",
                column: "PosteId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_ProjetId",
                table: "Equipements",
                column: "ProjetId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipements_StoreId",
                table: "Equipements",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Lignes_LigneId",
                table: "Equipements",
                column: "LigneId",
                principalTable: "Lignes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Postes_PosteId",
                table: "Equipements",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Projets_ProjetId",
                table: "Equipements",
                column: "ProjetId",
                principalTable: "Projets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipements_Stores_StoreId",
                table: "Equipements",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Lignes_LigneId",
                table: "Equipements");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Postes_PosteId",
                table: "Equipements");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Projets_ProjetId",
                table: "Equipements");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipements_Stores_StoreId",
                table: "Equipements");

            migrationBuilder.DropIndex(
                name: "IX_Equipements_LigneId",
                table: "Equipements");

            migrationBuilder.DropIndex(
                name: "IX_Equipements_PosteId",
                table: "Equipements");

            migrationBuilder.DropIndex(
                name: "IX_Equipements_ProjetId",
                table: "Equipements");

            migrationBuilder.DropIndex(
                name: "IX_Equipements_StoreId",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "CurrentEmplacement",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "LigneId",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "PosteId",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "ProjetId",
                table: "Equipements");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Equipements");
        }
    }
}
