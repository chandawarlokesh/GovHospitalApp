using System;
using System.Collections.Generic;

namespace GovHospitalApp.Core.Domain.Entities
{
    public class Hospital
    {
        public Hospital()
        {
            Patients = new HashSet<Patient>();
        }

        public Guid HospitalId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public Address Address { get; set; }

        // Navigational Property
        public virtual ICollection<Patient> Patients { get; private set; }

    }
}
