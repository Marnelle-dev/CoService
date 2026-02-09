using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeysInCertificats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Les FK ExportateurId, PartenaireId, ZoneProductionId existent déjà, on ne les supprime pas

            migrationBuilder.DropColumn(
                name: "Exportateur",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "Partenaire",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PaysDestination",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PortCongo",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PortSortie",
                table: "Certificats");

            migrationBuilder.AddColumn<Guid>(
                name: "DeviseId",
                table: "LignesCertificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IncotermId",
                table: "LignesCertificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionTarifaireId",
                table: "LignesCertificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniteStatistiqueId",
                table: "LignesCertificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BureauDedouanementId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeviseId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModuleId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaysDestinationId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PortCongoId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PortSortieId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_LignesCertificats_DeviseId",
                table: "LignesCertificats",
                column: "DeviseId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesCertificats_IncotermId",
                table: "LignesCertificats",
                column: "IncotermId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesCertificats_PositionTarifaireId",
                table: "LignesCertificats",
                column: "PositionTarifaireId");

            migrationBuilder.CreateIndex(
                name: "IX_LignesCertificats_UniteStatistiqueId",
                table: "LignesCertificats",
                column: "UniteStatistiqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_BureauDedouanementId",
                table: "Certificats",
                column: "BureauDedouanementId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_DeviseId",
                table: "Certificats",
                column: "DeviseId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_ModuleId",
                table: "Certificats",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_PaysDestinationId",
                table: "Certificats",
                column: "PaysDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_PortCongoId",
                table: "Certificats",
                column: "PortCongoId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_PortSortieId",
                table: "Certificats",
                column: "PortSortieId");


            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_BureauxDedouanements_BureauDedouanementId",
                table: "Certificats",
                column: "BureauDedouanementId",
                principalTable: "BureauxDedouanements",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Devises_DeviseId",
                table: "Certificats",
                column: "DeviseId",
                principalTable: "Devises",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            // La FK ExportateurId existe déjà, on ne la recrée pas

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Modules_ModuleId",
                table: "Certificats",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            // La FK PartenaireId existe déjà, on ne la recrée pas

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Pays_PaysDestinationId",
                table: "Certificats",
                column: "PaysDestinationId",
                principalTable: "Pays",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats",
                column: "PortCongoId",
                principalTable: "Ports",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats",
                column: "PortSortieId",
                principalTable: "Ports",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            // La FK ZoneProductionId existe déjà, on ne la recrée pas

            migrationBuilder.AddForeignKey(
                name: "FK_LignesCertificats_Devises_DeviseId",
                table: "LignesCertificats",
                column: "DeviseId",
                principalTable: "Devises",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesCertificats_Incoterms_IncotermId",
                table: "LignesCertificats",
                column: "IncotermId",
                principalTable: "Incoterms",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesCertificats_PositionsTariffaires_PositionTarifaireId",
                table: "LignesCertificats",
                column: "PositionTarifaireId",
                principalTable: "PositionsTariffaires",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesCertificats_UniteStatistiques_UniteStatistiqueId",
                table: "LignesCertificats",
                column: "UniteStatistiqueId",
                principalTable: "UniteStatistiques",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_BureauxDedouanements_BureauDedouanementId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Devises_DeviseId",
                table: "Certificats");

            // Les FK ExportateurId, PartenaireId, ZoneProductionId existent déjà

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Modules_ModuleId",
                table: "Certificats");


            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Pays_PaysDestinationId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats");


            migrationBuilder.DropForeignKey(
                name: "FK_LignesCertificats_Devises_DeviseId",
                table: "LignesCertificats");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesCertificats_Incoterms_IncotermId",
                table: "LignesCertificats");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesCertificats_PositionsTariffaires_PositionTarifaireId",
                table: "LignesCertificats");

            migrationBuilder.DropForeignKey(
                name: "FK_LignesCertificats_UniteStatistiques_UniteStatistiqueId",
                table: "LignesCertificats");

            migrationBuilder.DropIndex(
                name: "IX_LignesCertificats_DeviseId",
                table: "LignesCertificats");

            migrationBuilder.DropIndex(
                name: "IX_LignesCertificats_IncotermId",
                table: "LignesCertificats");

            migrationBuilder.DropIndex(
                name: "IX_LignesCertificats_PositionTarifaireId",
                table: "LignesCertificats");

            migrationBuilder.DropIndex(
                name: "IX_LignesCertificats_UniteStatistiqueId",
                table: "LignesCertificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_BureauDedouanementId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_DeviseId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_ModuleId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_PaysDestinationId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_PortCongoId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_PortSortieId",
                table: "Certificats");


            migrationBuilder.DropColumn(
                name: "DeviseId",
                table: "LignesCertificats");

            migrationBuilder.DropColumn(
                name: "IncotermId",
                table: "LignesCertificats");

            migrationBuilder.DropColumn(
                name: "PositionTarifaireId",
                table: "LignesCertificats");

            migrationBuilder.DropColumn(
                name: "UniteStatistiqueId",
                table: "LignesCertificats");

            migrationBuilder.DropColumn(
                name: "BureauDedouanementId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "DeviseId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ModuleId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PaysDestinationId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PortCongoId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PortSortieId",
                table: "Certificats");


            migrationBuilder.AddColumn<string>(
                name: "Exportateur",
                table: "Certificats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Partenaire",
                table: "Certificats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaysDestination",
                table: "Certificats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortCongo",
                table: "Certificats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PortSortie",
                table: "Certificats",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            // Les FK ExportateurId, PartenaireId, ZoneProductionId existent déjà
        }
    }
}
