using GovHospitalApp.Core.Domain.Enumerations;
using System;

namespace GovHospitalApp.Core.Domain.Entities
{
    public class Patient
    {
        public Patient()
        {
            Hospital = new Hospital();
        }

        public Guid PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public GenderType Gender { get; set; }
        public Address Address { get; set; }
        public string MobileNumber { get; set; }

        public Guid? HospitalId { get; set; }

        // Navigational Property
        public virtual Hospital Hospital { get; private set; }
    }
}
