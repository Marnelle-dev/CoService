using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatutCertificatTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Certificats_Statut",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "Statut",
                table: "Certificats");

            migrationBuilder.AddColumn<Guid>(
                name: "StatutCertificatId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatutsCertificats",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatutsCertificats", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_StatutCertificatId",
                table: "Certificats",
                column: "StatutCertificatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatutsCertificats_Code",
                table: "StatutsCertificats",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_StatutsCertificats_StatutCertificatId",
                table: "Certificats",
                column: "StatutCertificatId",
                principalTable: "StatutsCertificats",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_StatutsCertificats_StatutCertificatId",
                table: "Certificats");

            migrationBuilder.DropTable(
                name: "StatutsCertificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_StatutCertificatId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "StatutCertificatId",
                table: "Certificats");

            migrationBuilder.AddColumn<string>(
                name: "Statut",
                table: "Certificats",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Certificats_Statut",
                table: "Certificats",
                sql: "Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé', 'Rejeté', 'Modification', 'Formule A soumise', 'Formule A contrôlée', 'Formule A approuvée', 'Formule A validée')");
        }
    }
}
