using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    List<Doctor> GetDoctors();
    Doctor? GetDoctor(Guid id);
    void AddDoctor(Doctor doctor);
    void UpdateDoctor(Doctor doctor);
    void DeleteDoctor(Guid id);
    List<Speciality> GetSpecialities();
    Speciality? GetSpeciality(Guid id);
    void AddSpeciality(Speciality speciality);
    void UpdateSpeciality(Speciality speciality);
    void DeleteSpeciality(Guid id);
}

