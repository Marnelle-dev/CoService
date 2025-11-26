using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCertificateIdToAbonnement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_abonnements_AbonnementId",
                table: "certificates");

            migrationBuilder.DropCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates");

            migrationBuilder.RenameColumn(
                name: "AbonnementId",
                table: "certificates",
                newName: "abonnement_id");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_AbonnementId",
                table: "certificates",
                newName: "IX_certificates_abonnement_id");

            migrationBuilder.AddColumn<Guid>(
                name: "certificate_id",
                table: "abonnements",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates",
                sql: "Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé')");

            migrationBuilder.CreateIndex(
                name: "IX_abonnements_certificate_id",
                table: "abonnements",
                column: "certificate_id",
                unique: true,
                filter: "[certificate_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_abonnements_abonnement_id",
                table: "certificates",
                column: "abonnement_id",
                principalTable: "abonnements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements");

            migrationBuilder.DropForeignKey(
                name: "FK_certificates_abonnements_abonnement_id",
                table: "certificates");

            migrationBuilder.DropCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_abonnements_certificate_id",
                table: "abonnements");

            migrationBuilder.DropColumn(
                name: "certificate_id",
                table: "abonnements");

            migrationBuilder.RenameColumn(
                name: "abonnement_id",
                table: "certificates",
                newName: "AbonnementId");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_abonnement_id",
                table: "certificates",
                newName: "IX_certificates_AbonnementId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates",
                sql: "Statut IN ('Elabore', 'Soumis', 'Controle', 'Approuve', 'Valide')");

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_abonnements_AbonnementId",
                table: "certificates",
                column: "AbonnementId",
                principalTable: "abonnements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
