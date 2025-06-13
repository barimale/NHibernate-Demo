using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public interface IAddress2Repository : IRepository<Address2>
    {
        Task<Address2> GetByName(string name);
        Task<Company2?> AssignAddressToCompany(int addressId, int companyId);
    }
}