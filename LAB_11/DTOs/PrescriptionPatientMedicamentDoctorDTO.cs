using Microsoft.Build.Framework;

namespace LAB_11.DTOs;

public class PrescriptionPatientMedicamentDoctorDTO
{
    [Required]
    public PatientDTO Patient { get; set; }
    [Required]
    public DoctorDTO Doctor { get; set; }
    [Required]
    public List<MedicamentDTO> Medicaments { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    
}

public class PatientDTO
{
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public DateTime Birthdate { get; set; }
}

public class MedicamentDTO
{
    [Required]
    public int IdMedicament { get; set; }
    [Required]
    public string Name { get; set; }
    public int? Dose { get; set; }
    [Required]
    public string Description { get; set; }
}

public class DoctorDTO
{
    [Required]
    public int IdDoctor { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
}