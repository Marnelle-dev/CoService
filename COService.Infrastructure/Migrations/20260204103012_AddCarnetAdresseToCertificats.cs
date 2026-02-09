using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarnetAdresseToCertificats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ProductsRecipientAddress1",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ProductsRecipientAddress2",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ProductsRecipientName",
                table: "Certificats");

            migrationBuilder.AddColumn<Guid>(
                name: "CarnetAdresseId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarnetsAdresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RaisonSociale = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Coordonnees = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarnetsAdresses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_CarnetAdresseId",
                table: "Certificats",
                column: "CarnetAdresseId");

            migrationBuilder.CreateIndex(
                name: "IX_CarnetsAdresses_Nom",
                table: "CarnetsAdresses",
                column: "Nom");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_CarnetsAdresses_CarnetAdresseId",
                table: "Certificats",
                column: "CarnetAdresseId",
                principalTable: "CarnetsAdresses",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats",
                column: "PortCongoId",
                principalTable: "Ports",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats",
                column: "PortSortieId",
                principalTable: "Ports",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_CarnetsAdresses_CarnetAdresseId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats");

            migrationBuilder.DropTable(
                name: "CarnetsAdresses");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_CarnetAdresseId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "CarnetAdresseId",
                table: "Certificats");

            migrationBuilder.AddColumn<string>(
                name: "ProductsRecipientAddress1",
                table: "Certificats",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductsRecipientAddress2",
                table: "Certificats",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductsRecipientName",
                table: "Certificats",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortCongoId",
                table: "Certificats",
                column: "PortCongoId",
                principalTable: "Ports",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Ports_PortSortieId",
                table: "Certificats",
                column: "PortSortieId",
                principalTable: "Ports",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
