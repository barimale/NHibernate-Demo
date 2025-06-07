using NHibernate;
using NHibernate.Criterion;
using WebApplication1.Domain;
using WebApplication1.Repositories.DbContext;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public interface ICompany2Repository
    {
        Task<int> Add(Company2 product);
        Task Update(Company2 product);
        Task Remove(Company2 product);
        Task<Company2> GetById(int addressId);
    }

    public class Company2Repository : ICompany2Repository
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public Company2Repository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public async Task<int> Add(Company2 product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var result = await session.SaveAsync(product);
                await transaction.CommitAsync();

                return (int)result;
            }
        }
        
        public async Task Update(Company2 product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.UpdateAsync(product); 
                await transaction.CommitAsync(); 
            }
        }

        public async Task Remove(Company2 product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.DeleteAsync(product); 
                await transaction.CommitAsync(); 
            }
        } 
        
        public async Task<Company2> GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Company2>(productId);
            }
        }
    }
}
