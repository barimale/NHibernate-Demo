namespace Demo.Domain.ProductAggregate
{
    public interface IProductRepository
    {
        Task<int> Add(Product product);
        Task Update(Product product);
        Task Remove(Product product);
        Task<Product> GetById(int productId);
        Task<Product> GetByName(string name);
    }
}
