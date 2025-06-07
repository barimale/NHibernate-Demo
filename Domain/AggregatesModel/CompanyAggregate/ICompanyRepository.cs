namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public interface ICompanyRepository
    {
        Task<int> Add(Company product);
        Task<Company> GetById(int addressId);
        Task<Company> GetBySubquery();
        Task Remove(Company product);
        Task Update(Company product);
    }
}