using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Task<List<Doctor>> GetDoctorsAsync();
    Task<Doctor?> GetDoctorAsync(Guid id);
    Task AddDoctorAsync(Doctor doctor);
    Task UpdateDoctorAsync(Doctor doctor);
    Task DeleteDoctorAsync(Guid id);
    Task<List<Speciality>> GetSpecialitiesAsync();
    Task<Speciality?> GetSpecialityAsync(Guid id);
    Task AddSpecialityAsync(Speciality speciality);
    Task UpdateSpecialityAsync(Speciality speciality);
    Task DeleteSpecialityAsync(Guid id);
}

