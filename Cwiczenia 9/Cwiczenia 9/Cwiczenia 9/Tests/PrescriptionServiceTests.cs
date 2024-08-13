
namespace Cwiczenia_9.Tests;
using DTOs;
using Models;
using Repository;
using Services;

using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PrescriptionServiceTests
{
    private readonly PrescriptionService _prescriptionService;
    private readonly ApplicationDbContext _context;

    public PrescriptionServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        
        

        _context = new ApplicationDbContext(options);
        
        var doctor = new Doctor { DoctorId = 1, FirstName = "Dr. Smith", LastName = "Stank", Email = ":)"};
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        
        _prescriptionService = new PrescriptionService(_context);
    }

    [Fact]
    public async Task AddPrescriptionAsync_ShouldAddPrescription()
    {
        // Arrange
        var prescriptionDto = new PrescriptionDto
        {
            Date = new DateTime(2024, 1, 1),
            DueDate = new DateTime(2024, 1, 10),
            Patient = new PatientDto { PatientId = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1990, 1, 1) },
            IdDoctor = 1,
            Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto { MedicamentId = 1, Dose = 10, Description = "AAA" }
            }
        };

        // Act
        var result = await _prescriptionService.AddPrescriptionAsync(prescriptionDto);

        // Assert
        Assert.Equal("Prescription added successfully", result);
    }

    [Fact]
    public async Task GetPatientDetailsAsync_ShouldReturnPatientDetails()
    {
        // Arrange
        var patient = new Patient { PatientId = 1, FirstName = "Jan", LastName = "Kowalski", Birthdate = new DateTime(1990, 1, 1) };
        _context.Patients.Add(patient);
        _context.SaveChanges();

        // Act
        var result = await _prescriptionService.GetPatientDetailsAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Jan", result.FirstName);
    }
}
