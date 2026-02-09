using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDuplicateForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Supprimer les contraintes de clé étrangère si elles existent
            var sql = @"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Certificats_Exportateurs_ExportateurId1')
                    ALTER TABLE [Certificats] DROP CONSTRAINT [FK_Certificats_Exportateurs_ExportateurId1];
                
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Certificats_Partenaires_PartenaireId1')
                    ALTER TABLE [Certificats] DROP CONSTRAINT [FK_Certificats_Partenaires_PartenaireId1];
                
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Certificats_ZonesProductions_ZoneProductionId1')
                    ALTER TABLE [Certificats] DROP CONSTRAINT [FK_Certificats_ZonesProductions_ZoneProductionId1];
            ";
            migrationBuilder.Sql(sql);

            // Supprimer les index si ils existent
            var indexSql = @"
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Certificats_ExportateurId1' AND object_id = OBJECT_ID('Certificats'))
                    DROP INDEX [IX_Certificats_ExportateurId1] ON [Certificats];
                
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Certificats_PartenaireId1' AND object_id = OBJECT_ID('Certificats'))
                    DROP INDEX [IX_Certificats_PartenaireId1] ON [Certificats];
                
                IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Certificats_ZoneProductionId1' AND object_id = OBJECT_ID('Certificats'))
                    DROP INDEX [IX_Certificats_ZoneProductionId1] ON [Certificats];
            ";
            migrationBuilder.Sql(indexSql);

            // Supprimer les colonnes si elles existent
            var columnSql = @"
                IF EXISTS (SELECT * FROM sys.columns WHERE name = 'ExportateurId1' AND object_id = OBJECT_ID('Certificats'))
                    ALTER TABLE [Certificats] DROP COLUMN [ExportateurId1];
                
                IF EXISTS (SELECT * FROM sys.columns WHERE name = 'PartenaireId1' AND object_id = OBJECT_ID('Certificats'))
                    ALTER TABLE [Certificats] DROP COLUMN [PartenaireId1];
                
                IF EXISTS (SELECT * FROM sys.columns WHERE name = 'ZoneProductionId1' AND object_id = OBJECT_ID('Certificats'))
                    ALTER TABLE [Certificats] DROP COLUMN [ZoneProductionId1];
            ";
            migrationBuilder.Sql(columnSql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExportateurId1",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartenaireId1",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneProductionId1",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_ExportateurId1",
                table: "Certificats",
                column: "ExportateurId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_PartenaireId1",
                table: "Certificats",
                column: "PartenaireId1");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_ZoneProductionId1",
                table: "Certificats",
                column: "ZoneProductionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Exportateurs_ExportateurId1",
                table: "Certificats",
                column: "ExportateurId1",
                principalTable: "Exportateurs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Partenaires_PartenaireId1",
                table: "Certificats",
                column: "PartenaireId1",
                principalTable: "Partenaires",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_ZonesProductions_ZoneProductionId1",
                table: "Certificats",
                column: "ZoneProductionId1",
                principalTable: "ZonesProductions",
                principalColumn: "id");
        }
    }
}
