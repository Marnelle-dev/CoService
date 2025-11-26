using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "certificates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificateNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Exportateur = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Partenaire = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PaysDestination = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PortSortie = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PortCongo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Formule = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Mandataire = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductsRecipientName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductsRecipientAddress1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProductsRecipientAddress2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    navire = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    documents_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificates", x => x.id);
                    table.CheckConstraint("CK_certificates_Statut", "Statut IN ('Elabore', 'Soumis', 'Controle', 'Approuve', 'Valide')");
                });

            migrationBuilder.CreateTable(
                name: "abonnements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    certificate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    factureNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    formule = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    numero = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Statut = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abonnements", x => x.id);
                    table.ForeignKey(
                        name: "FK_abonnements_certificates_certificate_id",
                        column: x => x.certificate_id,
                        principalTable: "certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certificate_lines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    certificate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HSCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineNatureOfProduct = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LineQuantity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineUnits = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineGrossWeight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineNetWeight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineFOBValue = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LineVolume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificate_lines", x => x.id);
                    table.ForeignKey(
                        name: "FK_certificate_lines_certificates_certificate_id",
                        column: x => x.certificate_id,
                        principalTable: "certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "certificate_validations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    certificate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Etape = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleVisa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VisaPar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Commentaire = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_certificate_validations", x => x.id);
                    table.ForeignKey(
                        name: "FK_certificate_validations_certificates_certificate_id",
                        column: x => x.certificate_id,
                        principalTable: "certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "commentaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    certificate_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Commentaire = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_commentaires_certificates_certificate_id",
                        column: x => x.certificate_id,
                        principalTable: "certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_abonnements_certificate_id",
                table: "abonnements",
                column: "certificate_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificate_lines_certificate_id",
                table: "certificate_lines",
                column: "certificate_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificate_validations_certificate_id",
                table: "certificate_validations",
                column: "certificate_id");

            migrationBuilder.CreateIndex(
                name: "IX_certificates_CertificateNo",
                table: "certificates",
                column: "CertificateNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_commentaires_certificate_id",
                table: "commentaires",
                column: "certificate_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abonnements");

            migrationBuilder.DropTable(
                name: "certificate_lines");

            migrationBuilder.DropTable(
                name: "certificate_validations");

            migrationBuilder.DropTable(
                name: "commentaires");

            migrationBuilder.DropTable(
                name: "certificates");
        }
    }
}
