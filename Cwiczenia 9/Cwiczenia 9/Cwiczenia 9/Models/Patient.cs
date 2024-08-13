namespace Cwiczenia_9.Models;

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}