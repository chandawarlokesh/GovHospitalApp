using System;

namespace Domain.Entities
{
    public class Hospital
    {
        public Guid HospitalId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Address Address { get; set; }
    }
}