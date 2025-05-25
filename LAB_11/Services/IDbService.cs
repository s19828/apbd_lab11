using LAB_11.DTOs;
using LAB_11.Models;

namespace LAB_11.Services;

public interface IDbService
{
    Task<int> AddPrescription(PrescriptionPatientMedicamentDoctorDTO prescription);
    Task<PatientPrescriptionDTO> GetPatientById(int id);
}