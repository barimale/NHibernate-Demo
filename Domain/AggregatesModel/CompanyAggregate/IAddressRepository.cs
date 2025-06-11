using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetByCountry(string name);
        object AssingAddressToCompany(int addressId, int companyId, string description);
    }
}