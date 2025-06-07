namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public interface ICompany2Repository
    {
        Task<int> Add(Company2 product);
        Task<Company2> GetById(int addressId);
        Task Remove(Company2 product);
        Task Update(Company2 product);
    }
}