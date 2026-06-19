using System.Numerics;
using System.Text.Json;
using System.Xml.Linq;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private readonly List<Doctor> Doctores = [];
    private readonly List<Speciality> Specialities = [];

    public PersistenceInMemory()
    {
        InicializarDatos();
    }

    public List<Doctor> GetDoctors()
    {
        return Doctores;
    }

    public Doctor? GetDoctor(Guid id)
    {
        return Doctores.Find(d => d.Id == id);
    }

    public void AddDoctor(Doctor doctor)
    {
        Doctores.Add(doctor);
    }

    public void UpdateDoctor(Doctor doctor)
    {
        Doctor? doctorToUpdate = GetDoctor(doctor.Id);
        if (doctorToUpdate != null)
        {
            doctorToUpdate.Name = doctor.Name;
            doctorToUpdate.LicenseNumber = doctor.LicenseNumber;
            doctorToUpdate.Speciality = doctor.Speciality;
            doctorToUpdate.IsActive = doctor.IsActive;
        }
    }

    public void DeleteDoctor(Guid id)
    {
        Doctor? doctorToDelete = GetDoctor(id);
        if (doctorToDelete != null)
        {
            Doctores.Remove(doctorToDelete);
        }
    }

    public List<Speciality> GetSpecialities()
    {
        return Specialities;
    }
    public Speciality? GetSpeciality(Guid id)
    {
        return Specialities.Find(s => s.Id == id);
    }

    public void AddSpeciality(Speciality speciality)
    {
        Specialities.Add(speciality);
    }

    public void UpdateSpeciality(Speciality speciality)
    {
        Speciality? specialityToUpdate = GetSpeciality(speciality.Id);
        if (specialityToUpdate != null)
        {
            specialityToUpdate.Description = speciality.Description;
        }
    }

    public void DeleteSpeciality(Guid id)
    {
        Speciality? specialityToDelete = GetSpeciality(id);
        if (specialityToDelete != null)
        {
            Specialities.Remove(specialityToDelete);
        }
    }

    private void InicializaEspecialidades()
    {
        var specialitiesdata = LoadSpecialities("Specialities.json");
        if (specialitiesdata != null)
        {
            foreach (var data in specialitiesdata)
            {
                var speciality = new Speciality(data.Name, data.Description, data.Id);
                Specialities.Add(speciality);
            }
        }
    }

    private List<SpecialityDto>? LoadSpecialities(string? file)
    {
        if (file == null)
        {
            return null;
        }
        return LoadFromJson<SpecialityDto>(file);
    }


    private List<T>? LoadFromJson<T>(string pathfile)
    {
        string json = File.ReadAllTextAsync(pathfile);
        return JsonSerializer.Deserialize<List<T>>(json);
    }

    private void InicializarDatos() { }
}

