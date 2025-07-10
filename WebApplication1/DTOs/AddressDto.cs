using Demo.API.DTOs.Abstraction;

namespace Demo.API.DTOs
{
    public class AddressDto : BaseDto
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
    }
}
