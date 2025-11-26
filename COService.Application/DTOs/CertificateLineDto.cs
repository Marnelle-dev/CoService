namespace COService.Application.DTOs;

/// <summary>
/// DTO pour une ligne de certificat
/// </summary>
public class CertificateLineDto
{
    public Guid Id { get; set; }
    public Guid CertificateId { get; set; }
    public string? HSCode { get; set; }
    public string? LineNatureOfProduct { get; set; }
    public string? LineQuantity { get; set; }
    public string? LineUnits { get; set; }
    public string? LineGrossWeight { get; set; }
    public string? LineNetWeight { get; set; }
    public string? LineFOBValue { get; set; }
    public string? LineVolume { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}

/// <summary>
/// DTO pour créer une ligne de certificat
/// </summary>
public class CreerCertificateLineDto
{
    public string? HSCode { get; set; }
    public string? LineNatureOfProduct { get; set; }
    public string? LineQuantity { get; set; }
    public string? LineUnits { get; set; }
    public string? LineGrossWeight { get; set; }
    public string? LineNetWeight { get; set; }
    public string? LineFOBValue { get; set; }
    public string? LineVolume { get; set; }
}

/// <summary>
/// DTO pour modifier une ligne de certificat
/// </summary>
public class ModifierCertificateLineDto
{
    public string? HSCode { get; set; }
    public string? LineNatureOfProduct { get; set; }
    public string? LineQuantity { get; set; }
    public string? LineUnits { get; set; }
    public string? LineGrossWeight { get; set; }
    public string? LineNetWeight { get; set; }
    public string? LineFOBValue { get; set; }
    public string? LineVolume { get; set; }
}

