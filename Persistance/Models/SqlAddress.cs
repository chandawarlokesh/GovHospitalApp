using Domain.Entities;

namespace Persistence.Models
{
    public class SqlAddress
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address ToDomain()
        {
            return new Address
            {
                Street = Street,
                City = City,
                State = State,
                ZipCode = ZipCode
            };
        }

        public static SqlAddress FromDomain(Address address)
        {
            return new SqlAddress
            {
                Street = address.Street,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}