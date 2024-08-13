namespace Cwiczenia_9.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<PrescriptionMedicament> Prescription_Medicaments { get; set; }
}