using Demo.API.DTOs.Abstraction;
using Demo.Domain.AggregatesModel.Company2Aggregate;

namespace Demo.API.DTOs
{
    public class CompanyDto: BaseDto
    {
        public virtual string Foo { get; set; }
        public virtual IList<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }
}
