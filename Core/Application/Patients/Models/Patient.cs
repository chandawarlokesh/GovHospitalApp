using System;
using Domain.Enumerations;

namespace Application.Patients.Models
{
    public class Patient
    {
        public Patient(Guid id, string name, DateTime dateOfBirth, GenderType gender, Address address,
            string mobileNumber, Guid? hospitalId)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            MobileNumber = mobileNumber;
            HospitalId = hospitalId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public Address Address { get; set; }
        public string MobileNumber { get; set; }
        public Guid? HospitalId { get; set; }
    }
}