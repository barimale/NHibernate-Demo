using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public interface ICompanyRepository: IRepository<Company>
    {
        Task<Company> GetBySubquery();
    }
}