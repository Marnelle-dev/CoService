using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAbonnementModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =============================================
            // PARTIE 1: Création de la table certificate_types
            // =============================================
            migrationBuilder.CreateTable(
                name: "certificate_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    designation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificate_types", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_certificate_types_code",
                table: "certificate_types",
                column: "code",
                unique: true);

            // =============================================
            // PARTIE 2: Modification de la table certificates
            // =============================================
            // Ajouter la colonne TypeId
            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                table: "certificates",
                type: "uniqueidentifier",
                nullable: true);

            // Créer l'index pour TypeId
            migrationBuilder.CreateIndex(
                name: "IX_certificates_TypeId",
                table: "certificates",
                column: "TypeId");

            // Créer la clé étrangère vers certificate_types
            migrationBuilder.AddForeignKey(
                name: "FK_certificates_certificate_types_TypeId",
                table: "certificates",
                column: "TypeId",
                principalTable: "certificate_types",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            // Supprimer l'ancienne colonne Type (string)
            migrationBuilder.DropColumn(
                name: "Type",
                table: "certificates");

            // =============================================
            // PARTIE 3: Modification de la table abonnements
            // =============================================
            // Supprimer l'ancienne clé étrangère
            migrationBuilder.DropForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements");

            // Supprimer l'index sur certificate_id
            migrationBuilder.DropIndex(
                name: "IX_abonnements_certificate_id",
                table: "abonnements");

            // Supprimer l'ancienne colonne certificate_id
            migrationBuilder.DropColumn(
                name: "certificate_id",
                table: "abonnements");

            // Ajouter les nouvelles colonnes à abonnements
            migrationBuilder.AddColumn<string>(
                name: "exportateur",
                table: "abonnements",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "partenaire",
                table: "abonnements",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "type_co",
                table: "abonnements",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            // Ajouter la colonne AbonnementId à certificates
            migrationBuilder.AddColumn<Guid>(
                name: "AbonnementId",
                table: "certificates",
                type: "uniqueidentifier",
                nullable: true);

            // Créer l'index pour la nouvelle clé étrangère AbonnementId
            migrationBuilder.CreateIndex(
                name: "IX_certificates_AbonnementId",
                table: "certificates",
                column: "AbonnementId");

            // Créer la nouvelle clé étrangère de certificates vers abonnements
            migrationBuilder.AddForeignKey(
                name: "FK_certificates_abonnements_AbonnementId",
                table: "certificates",
                column: "AbonnementId",
                principalTable: "abonnements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // =============================================
            // PARTIE 3: Restaurer la table abonnements
            // =============================================
            // Supprimer la nouvelle clé étrangère
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_abonnements_AbonnementId",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_AbonnementId",
                table: "certificates");

            // Supprimer la colonne AbonnementId de certificates
            migrationBuilder.DropColumn(
                name: "AbonnementId",
                table: "certificates");

            // Supprimer les nouvelles colonnes de abonnements
            migrationBuilder.DropColumn(
                name: "type_co",
                table: "abonnements");

            migrationBuilder.DropColumn(
                name: "partenaire",
                table: "abonnements");

            migrationBuilder.DropColumn(
                name: "exportateur",
                table: "abonnements");

            // Restaurer l'ancienne colonne certificate_id
            migrationBuilder.AddColumn<Guid>(
                name: "certificate_id",
                table: "abonnements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Restaurer l'index
            migrationBuilder.CreateIndex(
                name: "IX_abonnements_certificate_id",
                table: "abonnements",
                column: "certificate_id");

            // Restaurer l'ancienne clé étrangère
            migrationBuilder.AddForeignKey(
                name: "FK_abonnements_certificates_certificate_id",
                table: "abonnements",
                column: "certificate_id",
                principalTable: "certificates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            // =============================================
            // PARTIE 2: Restaurer la table certificates
            // =============================================
            // Restaurer l'ancienne colonne Type (string)
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "certificates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            // Supprimer la clé étrangère vers certificate_types
            migrationBuilder.DropForeignKey(
                name: "FK_certificates_certificate_types_TypeId",
                table: "certificates");

            migrationBuilder.DropIndex(
                name: "IX_certificates_TypeId",
                table: "certificates");

            // Supprimer la colonne TypeId
            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "certificates");

            // =============================================
            // PARTIE 1: Supprimer la table certificate_types
            // =============================================
            migrationBuilder.DropTable(
                name: "certificate_types");
        }
    }
}

