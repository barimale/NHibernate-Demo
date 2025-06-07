namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public interface IAddress2Repository
    {
        Task<int> Add(Address2 product);
        Task<Address2> GetById(int addressId);
        Task<Address2> GetByName(string name);
        Task Remove(Address2 product);
        Task Update(Address2 product);
        void AssingAddressToCompany(int addressId, int companyId);
    }
}