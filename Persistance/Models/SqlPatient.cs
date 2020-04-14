using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities;
using Domain.Enumerations;

namespace Persistence.Models
{
    public class SqlPatient
    {
        [Required] public Guid Id { get; set; }

        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public SqlAddress Address { get; set; }
        public string MobileNumber { get; set; }

        [ForeignKey("Hospital")] public Guid? HospitalId { get; set; }

        // Navigational Property
        public virtual SqlHospital Hospital { get; set; }

        public Patient ToDomain()
        {
            return new Patient
            {
                PatientId = Id,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Gender = (GenderType) Gender,
                Address = Address.ToDomain(),
                MobileNumber = MobileNumber,
                HospitalId = HospitalId
            };
        }

        public static SqlPatient ToDomain(Patient patient)
        {
            return new SqlPatient
            {
                Id = patient.PatientId,
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth,
                Gender = (int) patient.Gender,
                Address = SqlAddress.FromDomain(patient.Address),
                MobileNumber = patient.MobileNumber,
                HospitalId = patient.HospitalId
            };
        }
    }
}