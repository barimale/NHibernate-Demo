using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<Product> GetByName(string name);
    }
}
