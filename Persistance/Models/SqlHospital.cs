using GovHospitalApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;

namespace GovHospitalApp.Core.Infrastructure.Persistance.Models
{
    public class SqlHospital
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public SqlAddress Address { get; set; }

        // Navigational Property
        public virtual ICollection<SqlPatient> Patients { get; set; }

        public Hospital ToDomain()
        =>
            new Hospital()
            {
                HospitalId = Id,
                Name = Name,
                MobileNumber = MobileNumber,
                Address = Address.ToDomain()
            };
        public static SqlHospital FromDomain(Hospital hospital)
        =>
            new SqlHospital()
            {
                Id = hospital.HospitalId,
                Name = hospital.Name,
                MobileNumber = hospital.MobileNumber,
                Address = SqlAddress.FromDomain(hospital.Address)
            };
    }
}
