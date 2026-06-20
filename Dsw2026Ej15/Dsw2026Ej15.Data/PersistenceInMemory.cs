using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;
using System.Text.Json;

namespace Dsw2026Ej15.Data;
public class PersistenceInMemory: IPersistence
{
    private readonly List<Doctor> Doctors = [];
    private readonly List<Speciality> Specialities = [];

    public PersistenceInMemory()
    {
        InicializarDatos();
    }

    public async Task<List<Doctor>> GetDoctorsAsync()
    {
        return await Task.FromResult(Doctors);
    }

    public async Task<Doctor?> GetDoctorAsync(Guid id)
    {
        return await Task.FromResult(Doctors.Find(d => d.Id == id));
    }

    public async Task AddDoctorAsync(Doctor doctor)
    {
        Doctors.Add(doctor);
    }

    public async Task UpdateDoctorAsync(Doctor doctor)
    {
        Doctor? doctorToUpdate = await GetDoctorAsync(doctor.Id);
        if (doctorToUpdate != null)
        {
            doctorToUpdate.Name = doctor.Name;
            doctorToUpdate.LicenseNumber = doctor.LicenseNumber;
            doctorToUpdate.Speciality = doctor.Speciality;
            doctorToUpdate.IsActive = doctor.IsActive;
        }
    }

    public async Task DeleteDoctorAsync(Guid id)
    {
        Doctor? doctorToDeactivate = await GetDoctorAsync(id);
        if (doctorToDeactivate != null && doctorToDeactivate.IsActive)
        {
            doctorToDeactivate.IsActive = false;
        }
    }

    public async Task<List<Speciality>> GetSpecialitiesAsync()
    {
        return await Task.FromResult(Specialities);
    }
    public async Task<Speciality?> GetSpecialityAsync(Guid id)
    {
        return await Task.FromResult(Specialities.Find(s => s.Id == id));
    }

    public async Task AddSpecialityAsync(Speciality speciality)
    {
        Specialities.Add(speciality);
    }

    public async Task UpdateSpecialityAsync(Speciality speciality)
    {
        Speciality? specialityToUpdate = await GetSpecialityAsync(speciality.Id);
        if (specialityToUpdate != null)
        {
            specialityToUpdate.Description = speciality.Description;
        }
    }

    public async Task DeleteSpecialityAsync(Guid id)
    {
        Speciality? specialityToDelete = await GetSpecialityAsync(id);
        if (specialityToDelete != null)
        {
            Specialities.Remove(specialityToDelete);
        }
    }

    private void InicializaEspecialidades()
    {
        var specialitiesdata = LoadFromJson<SpecialityDto>("Specialities.json");
        if (specialitiesdata != null)
        {
            foreach (var data in specialitiesdata)
            {
                var speciality = new Speciality(data.Name, data.Description, data.Id);
                Specialities.Add(speciality);
            }
        }
    }

    private List<T>? LoadFromJson<T>(string file)
    {
        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}");
        if (!File.Exists(jsonPath))
        {
            return null;
        }
        try
        {
            string json = File.ReadAllTextAsync(jsonPath).Result;
            return JsonSerializer.Deserialize<List<T>>(json);
        }
        catch (JsonException)
        {
            return null;
        }
        
    }

    private void InicializarDatos() 
    { 
        InicializaEspecialidades();
    }
}
