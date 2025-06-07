namespace Demo.Domain.CompanyAggregate
{
    public interface IAddressRepository
    {
        Task<int> Add(Address product);
        Task<Address> GetById(int addressId);
        Task<Address> GetByName(string name);
        Task Remove(Address product);
        Task Update(Address product);
    }
}