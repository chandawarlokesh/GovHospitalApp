using GovHospitalApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovHospitalApp.Core.Application.Interface
{
    public interface IAppDbRepository
    {
        #region Patient

        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<IEnumerable<Patient>> GetPatientsByHospitalIdAsync(Guid id);
        Task<Patient> GetPatientByIdAsync(Guid id);
        Task<Patient> GetPatientIdByMobileNumberAsync(string mobileNumber);
        Task AddPatientAsync(Patient patient);
        Task EditPatientByIdAsync(Guid id, Patient patient);
        Task RemovePatientByIdAsync(Guid id);

        #endregion

        #region Hospital

        Task<IEnumerable<Hospital>> GetHospitalsAsync();
        Task<Hospital> GetHospitalByIdAsync(Guid id);
        Task AddHospitalAsync(Hospital hospital);
        Task EditHospitalByIdAsync(Guid id, Hospital hospital);
        Task RemoveHospitalByIdAsync(Guid id);
        Task<Guid?> GetHospitalIdByZipCode(string zipCode);

        #endregion
    }
}
