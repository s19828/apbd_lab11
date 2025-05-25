using System.ComponentModel.DataAnnotations;

namespace LAB_11.DTOs;

public class PatientPrescriptionDTO
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
    public List<PrescriptionDTO> Prescriptions { get; set; }
}

public class PrescriptionDTO
{
    [Required]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DoctorDTO Doctor { get; set; }
}