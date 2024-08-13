using Cwiczenia_9.DTOs;
using Cwiczenia_9.Models;
using Cwiczenia_9.Repository;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia_9.Services;

public class PrescriptionService
{
    private readonly ApplicationDbContext _context;

    public PrescriptionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddPrescriptionAsync(PrescriptionDto prescriptionDto)
    {
        var patient = await _context.Patients.FindAsync(prescriptionDto.Patient.PatientId);
        if (patient == null)
        {
            patient = new Patient
            {
                PatientId = prescriptionDto.Patient.PatientId,
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                Birthdate = prescriptionDto.Patient.Birthdate
            };
            _context.Patients.Add(patient);
        }

        var doctor = await _context.Doctors.FindAsync(prescriptionDto.IdDoctor);
        if (doctor == null)
        {
            return "Doctor not found";
        }

        if (prescriptionDto.Medicaments.Count > 10)
        {
            return "A prescription cannot have more than 10 medicaments.";
        }

        if (prescriptionDto.DueDate < prescriptionDto.Date)
        {
            return "Due date must be greater than or equal to the prescription date.";
        }

        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            PatientId = patient.PatientId,
            DoctorId = doctor.DoctorId,
            Prescription_Medicaments = prescriptionDto.Medicaments.Select(m => new PrescriptionMedicament
            {
                MedicamentId = m.MedicamentId,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return "Prescription added successfully";
    }

    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Prescription_Medicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.PatientId == idPatient);

        if (patient == null)
        {
            return null;
        }

        var patientDetails = new PatientDetailsDto
        {
            IdPatient = patient.PatientId,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(pr => new PrescriptionDetailsDto
            {
                IdPrescription = pr.PrescriptionId,
                Date = pr.Date,
                DueDate = pr.DueDate,
                Medicaments = pr.Prescription_Medicaments.Select(pm => new MedicamentDto
                {
                    MedicamentId = pm.MedicamentId,
                    Name = pm.Medicament.Name,
                    Dose = pm.Dose,
                    Description = pm.Details
                }).ToList(),
                Doctor = new DoctorDto
                {
                    DoctorId = pr.Doctor.DoctorId,
                    FirstName = pr.Doctor.FirstName,
                    LastName = pr.Doctor.LastName,
                    Email = pr.Doctor.Email
                }
            }).OrderBy(pr => pr.DueDate).ToList()
        };

        return patientDetails;
    }
}