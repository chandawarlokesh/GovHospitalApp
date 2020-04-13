using System;

namespace GovHospitalApp.Core.Application.Infrastructure.Hospitals.Models
{
    public class Hospital
    {
        public Hospital(Guid id, string name, string mobileNumber, Address address)
        {
            Id = id;
            Name = name;
            MobileNumber = mobileNumber;
            Address = address;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Address Address { get; set; }
    }
}
