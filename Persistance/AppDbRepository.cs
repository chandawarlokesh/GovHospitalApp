using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class AppDbRepository : IAppDbRepository
    {
        private readonly AppDbContext _context;

        public AppDbRepository(AppDbContext context)
        {
            _context = context;
        }

        #region Patient

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            var patientList = await _context.Patients.ToListAsync();
            return patientList.Select(x => x.ToDomain());
        }

        public async Task<IEnumerable<Patient>> GetPatientsByHospitalIdAsync(Guid id)
        {
            var patientList = await _context.Patients.ToListAsync();
            var patientsInHospital = patientList.Where(x => x.HospitalId == id);
            return patientsInHospital.Select(x => x.ToDomain());
        }

        public async Task<Patient> GetPatientByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient?.ToDomain();
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(SqlPatient.ToDomain(patient));
            await _context.SaveChangesAsync();
        }

        public async Task EditPatientByIdAsync(Guid id, Patient patient)
        {
            var existingPatient = await _context.Patients.FindAsync(id);
            existingPatient.Name = patient.Name;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.MobileNumber = patient.MobileNumber;
            existingPatient.HospitalId = patient.HospitalId;
            existingPatient.Gender = (int) patient.Gender;
            existingPatient.Address = SqlAddress.FromDomain(patient.Address);

            // _context.Update(SqlPatient.ToDomain(patient));
            await _context.SaveChangesAsync();
        }

        public async Task RemovePatientByIdAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient> GetPatientIdByMobileNumberAsync(string mobileNumber)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x =>
                x.MobileNumber == mobileNumber);
            return patient?.ToDomain();
        }

        #endregion

        #region Hospital

        public async Task<IEnumerable<Hospital>> GetHospitalsAsync()
        {
            var hospitalList = await _context.Hospitals.ToListAsync();
            return hospitalList.Select(x => x.ToDomain());
        }

        public async Task<Hospital> GetHospitalByIdAsync(Guid id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            return hospital.ToDomain();
        }

        public async Task AddHospitalAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(SqlHospital.FromDomain(hospital));
            await _context.SaveChangesAsync();
        }

        public async Task EditHospitalByIdAsync(Guid id, Hospital hospital)
        {
            var existingHospital = await _context.Hospitals.FindAsync(id);
            existingHospital.Name = hospital.Name;
            existingHospital.MobileNumber = hospital.MobileNumber;
            existingHospital.Address = SqlAddress.FromDomain(hospital.Address);
            // _context.Update(SqlHospital.ToDomain(hospital));
            await _context.SaveChangesAsync();
        }

        public async Task RemoveHospitalByIdAsync(Guid id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid?> GetHospitalIdByZipCode(string zipCode)
        {
            var hospital = await _context.Hospitals.FirstOrDefaultAsync(x => x.Address.ZipCode == zipCode);
            return hospital?.Id;
        }

        #endregion
    }
}