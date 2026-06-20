namespace Dsw2026Ej15.Data.Dtos;
internal class DoctorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public Guid SpecialityGuid { get; set; }
    public bool IsActive { get; set; } = true;
}
