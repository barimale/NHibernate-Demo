using NHibernate;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public interface IProductRepository
    {
        Task<int> Add(Product product);
        Task Update(Product product);
        Task Remove(Product product);
        Task<Product> GetById(int productId);
        Task<Product> GetByName(string name);
    }

    public class ProductRepository : IProductRepository 
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public ProductRepository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public async Task<int> Add(Product product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var result = await session.SaveAsync(product);
                await transaction.CommitAsync();

                return (int)result;
            }
        }
        
        public async Task Update(Product product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.UpdateAsync(product); 
                await transaction.CommitAsync(); 
            }
        }

        public async Task Remove(Product product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.DeleteAsync(product); 
                await transaction.CommitAsync(); 
            }
        } 
        
        public async Task<Product> GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Product>(productId);
            }
        }

        public async Task<Product> GetByName(string name)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                // Corrected to use QueryOver for LINQ-like querying
                Product product = await session.QueryOver<Product>()
                    .Where(p => p.Name == name)
                    .SingleOrDefaultAsync();

                return product;
            }
        }
    }
}
