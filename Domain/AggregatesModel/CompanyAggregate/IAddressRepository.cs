namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public interface IAddressRepository
    {
        Task<int> Add(Address product);
        Task<Address> GetById(int addressId);
        Task<Address> GetByCountry(string name);
        Task Remove(Address product);
        Task Update(Address product);
        object AssingAddressToCompany(int addressId, int companyId, string description);

    }
}