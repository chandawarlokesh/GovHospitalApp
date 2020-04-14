using System;
using Domain.Enumerations;

namespace Domain.Entities
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public Address Address { get; set; }
        public string MobileNumber { get; set; }
        public Guid? HospitalId { get; set; }
    }
}