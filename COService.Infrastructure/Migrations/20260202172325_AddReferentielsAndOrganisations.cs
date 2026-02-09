using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReferentielsAndOrganisations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExportateurId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PartenaireId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ZoneProductionId",
                table: "Certificats",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BureauxDedouanements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BureauxDedouanements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Corridors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corridors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Departements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departements", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Devises",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Symbole = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoutesNationales",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutesNationales", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SectionsTariffaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionsTariffaires", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypesPartenaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesPartenaires", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UniteStatistiques",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniteStatistiques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TauxDeChanges",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Taux = table.Column<decimal>(type: "decimal(20,5)", nullable: false),
                    ValideDe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TauxDeChanges", x => x.id);
                    table.ForeignKey(
                        name: "FK_TauxDeChanges_Devises_DeviseId",
                        column: x => x.DeviseId,
                        principalTable: "Devises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incoterms",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incoterms", x => x.id);
                    table.ForeignKey(
                        name: "FK_Incoterms_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Aeroports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaysId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aeroports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Aeroports_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaysId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ports_Pays_PaysId",
                        column: x => x.PaysId,
                        principalTable: "Pays",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Troncons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CorridorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RouteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Troncons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Troncons_Corridors_CorridorId",
                        column: x => x.CorridorId,
                        principalTable: "Corridors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Troncons_RoutesNationales_RouteId",
                        column: x => x.RouteId,
                        principalTable: "RoutesNationales",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ChapitresTariffaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SectionTarifaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChapitresTariffaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_ChapitresTariffaires_SectionsTariffaires_SectionTarifaireId",
                        column: x => x.SectionTarifaireId,
                        principalTable: "SectionsTariffaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Partenaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodePartenaire = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TypePartenaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DerniereSynchronisation = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partenaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_Partenaires_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Partenaires_TypesPartenaires_TypePartenaireId",
                        column: x => x.TypePartenaireId,
                        principalTable: "TypesPartenaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DivisionsTariffaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ChapitreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionsTariffaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_DivisionsTariffaires_ChapitresTariffaires_ChapitreId",
                        column: x => x.ChapitreId,
                        principalTable: "ChapitresTariffaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Exportateurs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeExportateur = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RaisonSociale = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NIU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RCCM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeActivite = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PartenaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeExportateur = table.Column<int>(type: "int", nullable: true),
                    DerniereSynchronisation = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exportateurs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Exportateurs_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "Departements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Exportateurs_Partenaires_PartenaireId",
                        column: x => x.PartenaireId,
                        principalTable: "Partenaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ZonesProductions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartenaireId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonesProductions", x => x.id);
                    table.ForeignKey(
                        name: "FK_ZonesProductions_Partenaires_PartenaireId",
                        column: x => x.PartenaireId,
                        principalTable: "Partenaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesTariffaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DivisionCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DivisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesTariffaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_CategoriesTariffaires_DivisionsTariffaires_DivisionId",
                        column: x => x.DivisionId,
                        principalTable: "DivisionsTariffaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PositionsTariffaires",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategorieCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Actif = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreeLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    CreePar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierLe = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ModifiePar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionsTariffaires", x => x.id);
                    table.ForeignKey(
                        name: "FK_PositionsTariffaires_CategoriesTariffaires_CategorieCodeId",
                        column: x => x.CategorieCodeId,
                        principalTable: "CategoriesTariffaires",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_ExportateurId",
                table: "Certificats",
                column: "ExportateurId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_PartenaireId",
                table: "Certificats",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificats_ZoneProductionId",
                table: "Certificats",
                column: "ZoneProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_Aeroports_Code",
                table: "Aeroports",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aeroports_PaysId",
                table: "Aeroports",
                column: "PaysId");

            migrationBuilder.CreateIndex(
                name: "IX_BureauxDedouanements_Code",
                table: "BureauxDedouanements",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesTariffaires_Code",
                table: "CategoriesTariffaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesTariffaires_DivisionId",
                table: "CategoriesTariffaires",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ChapitresTariffaires_Code",
                table: "ChapitresTariffaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChapitresTariffaires_SectionTarifaireId",
                table: "ChapitresTariffaires",
                column: "SectionTarifaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Corridors_Code",
                table: "Corridors",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departements_Code",
                table: "Departements",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devises_Code",
                table: "Devises",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionsTariffaires_ChapitreId",
                table: "DivisionsTariffaires",
                column: "ChapitreId");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionsTariffaires_Code",
                table: "DivisionsTariffaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exportateurs_CodeExportateur",
                table: "Exportateurs",
                column: "CodeExportateur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exportateurs_DepartementId",
                table: "Exportateurs",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Exportateurs_PartenaireId",
                table: "Exportateurs",
                column: "PartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Incoterms_Code",
                table: "Incoterms",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incoterms_ModuleId",
                table: "Incoterms",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Code",
                table: "Modules",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partenaires_CodePartenaire",
                table: "Partenaires",
                column: "CodePartenaire",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partenaires_DepartementId",
                table: "Partenaires",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_Partenaires_TypePartenaireId",
                table: "Partenaires",
                column: "TypePartenaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Pays_Code",
                table: "Pays",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ports_Code",
                table: "Ports",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ports_PaysId",
                table: "Ports",
                column: "PaysId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionsTariffaires_CategorieCodeId",
                table: "PositionsTariffaires",
                column: "CategorieCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_PositionsTariffaires_Code",
                table: "PositionsTariffaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutesNationales_Code",
                table: "RoutesNationales",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_Code",
                table: "Sections",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionsTariffaires_Code",
                table: "SectionsTariffaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TauxDeChanges_DeviseId",
                table: "TauxDeChanges",
                column: "DeviseId");

            migrationBuilder.CreateIndex(
                name: "IX_Troncons_Code",
                table: "Troncons",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Troncons_CorridorId",
                table: "Troncons",
                column: "CorridorId");

            migrationBuilder.CreateIndex(
                name: "IX_Troncons_RouteId",
                table: "Troncons",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_TypesPartenaires_Code",
                table: "TypesPartenaires",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UniteStatistiques_Code",
                table: "UniteStatistiques",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZonesProductions_PartenaireId",
                table: "ZonesProductions",
                column: "PartenaireId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Exportateurs_ExportateurId",
                table: "Certificats",
                column: "ExportateurId",
                principalTable: "Exportateurs",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_Partenaires_PartenaireId",
                table: "Certificats",
                column: "PartenaireId",
                principalTable: "Partenaires",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificats_ZonesProductions_ZoneProductionId",
                table: "Certificats",
                column: "ZoneProductionId",
                principalTable: "ZonesProductions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Exportateurs_ExportateurId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_Partenaires_PartenaireId",
                table: "Certificats");

            migrationBuilder.DropForeignKey(
                name: "FK_Certificats_ZonesProductions_ZoneProductionId",
                table: "Certificats");

            migrationBuilder.DropTable(
                name: "Aeroports");

            migrationBuilder.DropTable(
                name: "BureauxDedouanements");

            migrationBuilder.DropTable(
                name: "Exportateurs");

            migrationBuilder.DropTable(
                name: "Incoterms");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "PositionsTariffaires");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "TauxDeChanges");

            migrationBuilder.DropTable(
                name: "Troncons");

            migrationBuilder.DropTable(
                name: "UniteStatistiques");

            migrationBuilder.DropTable(
                name: "ZonesProductions");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "CategoriesTariffaires");

            migrationBuilder.DropTable(
                name: "Devises");

            migrationBuilder.DropTable(
                name: "Corridors");

            migrationBuilder.DropTable(
                name: "RoutesNationales");

            migrationBuilder.DropTable(
                name: "Partenaires");

            migrationBuilder.DropTable(
                name: "DivisionsTariffaires");

            migrationBuilder.DropTable(
                name: "Departements");

            migrationBuilder.DropTable(
                name: "TypesPartenaires");

            migrationBuilder.DropTable(
                name: "ChapitresTariffaires");

            migrationBuilder.DropTable(
                name: "SectionsTariffaires");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_ExportateurId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_PartenaireId",
                table: "Certificats");

            migrationBuilder.DropIndex(
                name: "IX_Certificats_ZoneProductionId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ExportateurId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "PartenaireId",
                table: "Certificats");

            migrationBuilder.DropColumn(
                name: "ZoneProductionId",
                table: "Certificats");
        }
    }
}
