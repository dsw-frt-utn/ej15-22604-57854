using Dsw2026Ej15.Api.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Domain.Validators;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Dsw2026Ej15.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistencia;

    public DoctorsController(IPersistence persistencia)
    {
        _persistencia = persistencia;
    }

    //Post api/doctors
    [HttpPost]
    public async Task<ActionResult> CreateDoctorAsync([FromBody] CreateDoctorDto dto)
    {
        Speciality? speciality = await _persistencia.GetSpecialityAsync(dto.SpecialityId);
        List<string> errors = DoctorsValidator.ValidateNew(dto.Name, dto.LicenseNumber, speciality);
        if (errors.Count > 0)
        {
            throw new ValidationException(string.Join("-", errors));
        }
        var doctor = new Doctor(dto.Name, dto.LicenseNumber, speciality);
        await _persistencia.AddDoctorAsync(doctor);
        return Created();
    }

    //Get api/doctors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctorsAsync()
    {
        var activeDoctors = DoctorsValidator.ActiveDoctors(await _persistencia.GetDoctorsAsync());
        return Ok(activeDoctors);
    }

    //Get api/doctors/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Doctor>> GetDoctorAsync(Guid id)
    {
        var doctor = await _persistencia.GetDoctorAsync(id);
        if (!DoctorsValidator.ActiveDoctor(doctor))
        {
            throw new NotFoundException("Doctor no encontrado o inactivo");
        }
        return Ok(new 
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialitiName = doctor.Speciality.Name
        });
    }

    //Delete api/doctors/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDoctorAsync(Guid id)
    {
        var doctor = await _persistencia.GetDoctorAsync(id);
        if (!DoctorsValidator.ActiveDoctor(doctor))
        {
            throw new NotFoundException("Doctor no encontrado o inactivo");
        }
        await _persistencia.DeleteDoctorAsync(doctor.Id);
        return NoContent();
    }
}
