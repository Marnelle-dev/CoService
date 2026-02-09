using AutoMapper;
using COService.Application.DTOs;
using COService.Domain.Entities;

namespace COService.Application.Mappings;

/// <summary>
/// Profil AutoMapper pour les mappings entre Domain et DTOs
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CertificatOrigine
        CreateMap<CertificatOrigine, CertificatOrigineDto>()
            .ForMember(dest => dest.StatutCertificatId, opt => opt.MapFrom(src => src.StatutCertificatId))
            .ForMember(dest => dest.StatutNom, opt => opt.MapFrom(src => src.StatutCertificat != null ? src.StatutCertificat.Nom : null));

        CreateMap<CreerCertificatOrigineDto, CertificatOrigine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.StatutCertificatId, opt => opt.Ignore()) // Sera défini par le service
            .ForMember(dest => dest.StatutCertificat, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificateLines, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateValidations, opt => opt.Ignore())
            .ForMember(dest => dest.Commentaires, opt => opt.Ignore())
            .ForMember(dest => dest.Abonnement, opt => opt.Ignore())
            .ForMember(dest => dest.AbonnementId, opt => opt.Ignore())
            // Ignorer les propriétés de navigation (seuls les IDs sont mappés)
            .ForMember(dest => dest.Exportateur, opt => opt.Ignore())
            .ForMember(dest => dest.Partenaire, opt => opt.Ignore())
            .ForMember(dest => dest.PaysDestination, opt => opt.Ignore())
            .ForMember(dest => dest.PortSortie, opt => opt.Ignore())
            .ForMember(dest => dest.PortCongo, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.ZoneProduction, opt => opt.Ignore())
            .ForMember(dest => dest.BureauDedouanement, opt => opt.Ignore())
            .ForMember(dest => dest.Module, opt => opt.Ignore())
            .ForMember(dest => dest.Devise, opt => opt.Ignore())
            .ForMember(dest => dest.CarnetAdresse, opt => opt.Ignore());

        CreateMap<ModifierCertificatOrigineDto, CertificatOrigine>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateNo, opt => opt.Ignore())
            .ForMember(dest => dest.StatutCertificatId, opt => opt.Ignore())
            .ForMember(dest => dest.StatutCertificat, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificateLines, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateValidations, opt => opt.Ignore())
            .ForMember(dest => dest.Commentaires, opt => opt.Ignore())
            .ForMember(dest => dest.Abonnement, opt => opt.Ignore())
            .ForMember(dest => dest.AbonnementId, opt => opt.Ignore())
            // Ignorer les propriétés de navigation (seuls les IDs sont mappés)
            .ForMember(dest => dest.Exportateur, opt => opt.Ignore())
            .ForMember(dest => dest.Partenaire, opt => opt.Ignore())
            .ForMember(dest => dest.PaysDestination, opt => opt.Ignore())
            .ForMember(dest => dest.PortSortie, opt => opt.Ignore())
            .ForMember(dest => dest.PortCongo, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.Ignore())
            .ForMember(dest => dest.ZoneProduction, opt => opt.Ignore())
            .ForMember(dest => dest.BureauDedouanement, opt => opt.Ignore())
            .ForMember(dest => dest.Module, opt => opt.Ignore())
            .ForMember(dest => dest.Devise, opt => opt.Ignore())
            .ForMember(dest => dest.CarnetAdresse, opt => opt.Ignore());

        // CertificateLine
        CreateMap<CertificateLine, CertificateLineDto>();
        CreateMap<CreerCertificateLineDto, CertificateLine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificatOrigine, opt => opt.Ignore());

        CreateMap<ModifierCertificateLineDto, CertificateLine>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificatOrigine, opt => opt.Ignore());

        // Abonnement
        CreateMap<Abonnement, AbonnementDto>()
            .ForMember(dest => dest.NombreCertificats, opt => opt.MapFrom(src => src.Certificats.Count));

        CreateMap<CreerAbonnementDto, Abonnement>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Certificats, opt => opt.Ignore());

        CreateMap<ModifierAbonnementDto, Abonnement>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Certificats, opt => opt.Ignore());

        // Commentaire
        CreateMap<Commentaire, CommentaireDto>();
        CreateMap<CreerCommentaireDto, Commentaire>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificatOrigine, opt => opt.Ignore());

        CreateMap<ModifierCommentaireDto, Commentaire>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateId, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificatOrigine, opt => opt.Ignore());

        // CertificateValidation
        CreateMap<CertificateValidation, CertificateValidationDto>()
            .ForMember(dest => dest.Etape, opt => opt.MapFrom(src => src.Etape.ToString()))
            .ForMember(dest => dest.RoleVisa, opt => opt.MapFrom(src => src.RoleVisa.ToString()));

        // CertificateType
        CreateMap<CertificateType, CertificateTypeDto>()
            .ForMember(dest => dest.NombreCertificats, opt => opt.MapFrom(src => src.Certificats.Count));

        CreateMap<CreerCertificateTypeDto, CertificateType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Certificats, opt => opt.Ignore());

        CreateMap<ModifierCertificateTypeDto, CertificateType>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Certificats, opt => opt.Ignore());

        // Partenaire
        CreateMap<Partenaire, PartenaireDto>()
            .ForMember(dest => dest.TypePartenaireNom, opt => opt.Ignore())
            .ForMember(dest => dest.DepartementNom, opt => opt.Ignore());

        // Mapping inverse pour la synchronisation (DTO vers Entity)
        CreateMap<PartenaireDto, Partenaire>()
            .ForMember(dest => dest.TypePartenaire, opt => opt.Ignore())
            .ForMember(dest => dest.Departement, opt => opt.Ignore())
            .ForMember(dest => dest.Certificats, opt => opt.Ignore())
            .ForMember(dest => dest.Exportateurs, opt => opt.Ignore())
            .ForMember(dest => dest.ZonesProductions, opt => opt.Ignore());

        // Exportateur
        CreateMap<Exportateur, ExportateurDto>()
            .ForMember(dest => dest.PartenaireNom, opt => opt.Ignore())
            .ForMember(dest => dest.DepartementNom, opt => opt.Ignore());

        // Mapping inverse pour la synchronisation (DTO vers Entity)
        CreateMap<ExportateurDto, Exportateur>()
            .ForMember(dest => dest.Partenaire, opt => opt.Ignore())
            .ForMember(dest => dest.Departement, opt => opt.Ignore())
            .ForMember(dest => dest.Certificats, opt => opt.Ignore());

        // TypePartenaire
        CreateMap<TypePartenaire, TypePartenaireDto>();

        // StatutCertificat
        CreateMap<StatutCertificat, StatutCertificatDto>();
    }
}

