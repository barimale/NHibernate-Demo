namespace Demo.Domain.Abstraction
{
    public interface IRepository<T>
        where T: class
    {
        Task<int> Add(T product);
        Task Update(T product);
        Task Remove(T product);
        Task<T> GetById(int productId);
    }
}
