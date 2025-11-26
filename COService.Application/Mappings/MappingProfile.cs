using AutoMapper;
using COService.Application.DTOs;
using COService.Domain.Entities;
using COService.Domain.Enums;

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
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Statut.ToString()));

        CreateMap<CreerCertificatOrigineDto, CertificatOrigine>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => StatutCertificat.Elabore))
            .ForMember(dest => dest.CreeLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificateLines, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateValidations, opt => opt.Ignore())
            .ForMember(dest => dest.Commentaires, opt => opt.Ignore())
            .ForMember(dest => dest.Abonnement, opt => opt.Ignore())
            .ForMember(dest => dest.AbonnementId, opt => opt.Ignore());

        CreateMap<ModifierCertificatOrigineDto, CertificatOrigine>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateNo, opt => opt.Ignore())
            .ForMember(dest => dest.Statut, opt => opt.Ignore())
            .ForMember(dest => dest.CreeLe, opt => opt.Ignore())
            .ForMember(dest => dest.CreePar, opt => opt.Ignore())
            .ForMember(dest => dest.ModifierLe, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.CertificateLines, opt => opt.Ignore())
            .ForMember(dest => dest.CertificateValidations, opt => opt.Ignore())
            .ForMember(dest => dest.Commentaires, opt => opt.Ignore())
            .ForMember(dest => dest.Abonnement, opt => opt.Ignore())
            .ForMember(dest => dest.AbonnementId, opt => opt.Ignore());

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
    }
}

