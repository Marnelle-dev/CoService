using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesToFrenchPascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements");

            migrationBuilder.DropForeignKey(
                name: "FK_certificate_lines_certificates_certificate_id",
                table: "certificate_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_certificate_validations_certificates_certificate_id",
                table: "certificate_validations");

            migrationBuilder.DropForeignKey(
                name: "FK_certificates_abonnements_abonnement_id",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_certificates_certificate_types_TypeId",
                table: "certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_commentaires_certificates_certificate_id",
                table: "commentaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_commentaires",
                table: "commentaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_abonnements",
                table: "abonnements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificates",
                table: "certificates");

            migrationBuilder.DropCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificate_validations",
                table: "certificate_validations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificate_types",
                table: "certificate_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certificate_lines",
                table: "certificate_lines");

            migrationBuilder.RenameTable(
                name: "commentaires",
                newName: "Commentaires");

            migrationBuilder.RenameTable(
                name: "abonnements",
                newName: "Abonnements");

            migrationBuilder.RenameTable(
                name: "certificates",
                newName: "Certificats");

            migrationBuilder.RenameTable(
                name: "certificate_validations",
                newName: "ValidationsCertificats");

            migrationBuilder.RenameTable(
                name: "certificate_types",
                newName: "TypesCertificats");

            migrationBuilder.RenameTable(
                name: "certificate_lines",
                newName: "LignesCertificats");

            migrationBuilder.RenameIndex(
                name: "IX_commentaires_certificate_id",
                table: "Commentaires",
                newName: "IX_Commentaires_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_abonnements_certificate_id",
                table: "Abonnements",
                newName: "IX_Abonnements_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_TypeId",
                table: "Certificats",
                newName: "IX_Certificats_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_CertificateNo",
                table: "Certificats",
                newName: "IX_Certificats_CertificateNo");

            migrationBuilder.RenameIndex(
                name: "IX_certificates_abonnement_id",
                table: "Certificats",
                newName: "IX_Certificats_abonnement_id");

            migrationBuilder.RenameIndex(
                name: "IX_certificate_validations_certificate_id",
                table: "ValidationsCertificats",
                newName: "IX_ValidationsCertificats_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_certificate_types_code",
                table: "TypesCertificats",
                newName: "IX_TypesCertificats_code");

            migrationBuilder.RenameIndex(
                name: "IX_certificate_lines_certificate_id",
                table: "LignesCertificats",
                newName: "IX_LignesCertificats_certificate_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Commentaires",
                table: "Commentaires",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Abonnements",
                table: "Abonnements",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certificats",
                table: "Certificats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationsCertificats",
                table: "ValidationsCertificats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypesCertificats",
                table: "TypesCertificats",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LignesCertificats",
                table: "LignesCertificats",
                column: "id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Certificats_Statut",
                table: "Certificats",
                sql: "Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé', 'Rejeté', 'Modification', 'Formule A soumise', 'Formule A contrôlée', 'Formule A approuvée', 'Formule A validée')");

            migrationBuilder.AddForeignKey(
                name: "FK_Abonnements_Certificats_certificate_id",
                table: "Abonnements",
                column: "certificate_id",
                principalTable: "Certificats",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Abonnements_abonnement_id",
                table: "Certificats",
                column: "abonnement_id",
                principalTable: "Abonnements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_TypesCertificats_TypeId",
                table: "Certificats",
                column: "TypeId",
                principalTable: "TypesCertificats",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Commentaires_Certificats_certificate_id",
                table: "Commentaires",
                column: "certificate_id",
                principalTable: "Certificats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesCertificats_Certificats_certificate_id",
                table: "LignesCertificats",
                column: "certificate_id",
                principalTable: "Certificats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ValidationsCertificats_Certificats_certificate_id",
                table: "ValidationsCertificats",
                column: "certificate_id",
                principalTable: "Certificats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonnements_Certificats_certificate_id",
                table: "Abonnements");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Abonnements_abonnement_id",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_TypesCertificats_TypeId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Commentaires_Certificats_certificate_id",
                table: "Commentaires");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesCertificats_Certificats_certificate_id",
                table: "LignesCertificats");

            migrationBuilder.DropForeignKey(
                name: "FK_ValidationsCertificats_Certificats_certificate_id",
                table: "ValidationsCertificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Commentaires",
                table: "Commentaires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Abonnements",
                table: "Abonnements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationsCertificats",
                table: "ValidationsCertificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypesCertificats",
                table: "TypesCertificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LignesCertificats",
                table: "LignesCertificats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certificats",
                table: "Certificats");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Certificats_Statut",
                table: "Certificats");

            migrationBuilder.RenameTable(
                name: "Commentaires",
                newName: "commentaires");

            migrationBuilder.RenameTable(
                name: "Abonnements",
                newName: "abonnements");

            migrationBuilder.RenameTable(
                name: "ValidationsCertificats",
                newName: "certificate_validations");

            migrationBuilder.RenameTable(
                name: "TypesCertificats",
                newName: "certificate_types");

            migrationBuilder.RenameTable(
                name: "LignesCertificats",
                newName: "certificate_lines");

            migrationBuilder.RenameTable(
                name: "Certificats",
                newName: "certificates");

            migrationBuilder.RenameIndex(
                name: "IX_Commentaires_certificate_id",
                table: "commentaires",
                newName: "IX_commentaires_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_Abonnements_certificate_id",
                table: "abonnements",
                newName: "IX_abonnements_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_ValidationsCertificats_certificate_id",
                table: "certificate_validations",
                newName: "IX_certificate_validations_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_TypesCertificats_code",
                table: "certificate_types",
                newName: "IX_certificate_types_code");

            migrationBuilder.RenameIndex(
                name: "IX_LignesCertificats_certificate_id",
                table: "certificate_lines",
                newName: "IX_certificate_lines_certificate_id");

            migrationBuilder.RenameIndex(
                name: "IX_Certificats_TypeId",
                table: "certificates",
                newName: "IX_certificates_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Certificats_CertificateNo",
                table: "certificates",
                newName: "IX_certificates_CertificateNo");

            migrationBuilder.RenameIndex(
                name: "IX_Certificats_abonnement_id",
                table: "certificates",
                newName: "IX_certificates_abonnement_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_commentaires",
                table: "commentaires",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_abonnements",
                table: "abonnements",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificate_validations",
                table: "certificate_validations",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificate_types",
                table: "certificate_types",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificate_lines",
                table: "certificate_lines",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certificates",
                table: "certificates",
                column: "id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_certificates_Statut",
                table: "certificates",
                sql: "Statut IN ('Élaboré', 'Soumis', 'Contrôlé', 'Approuvé', 'Validé')");

            migrationBuilder.AddForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_certificate_lines_certificates_certificate_id",
                table: "certificate_lines",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_certificate_validations_certificates_certificate_id",
                table: "certificate_validations",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_abonnements_abonnement_id",
                table: "certificates",
                column: "abonnement_id",
                principalTable: "abonnements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_certificates_certificate_types_TypeId",
                table: "certificates",
                column: "TypeId",
                principalTable: "certificate_types",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_commentaires_certificates_certificate_id",
                table: "commentaires",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
