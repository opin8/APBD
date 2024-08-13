using Cwiczenia_9.DTOs;
using Cwiczenia_9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia_9.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController : ControllerBase
{
    private readonly PrescriptionService _prescriptionService;

    public PrescriptionController(PrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionDto prescriptionDto)
    {
        var result = await _prescriptionService.AddPrescriptionAsync(prescriptionDto);
        if (result == "Prescription added successfully")
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("{idPatient}")]
    public async Task<IActionResult> GetPatientDetails(int idPatient)
    {
        var patientDetails = await _prescriptionService.GetPatientDetailsAsync(idPatient);
        if (patientDetails == null)
        {
            return NotFound();
        }
        return Ok(patientDetails);
    }
}
