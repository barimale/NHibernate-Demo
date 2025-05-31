using NHibernate;
using NHibernate.Criterion;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public interface ICompanyRepository
    {
        Task<int> Add(Company product);
        Task Update(Company product);
        Task Remove(Company product);
        Task<Company> GetById(int addressId);
        Task<Company> GetBySubquery();
    }

    public class CompanyRepository : ICompanyRepository
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public CompanyRepository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public async Task<int> Add(Company product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var result = await session.SaveAsync(product);
                await transaction.CommitAsync();

                return (int)result;
            }
        }
        
        public async Task Update(Company product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.UpdateAsync(product); 
                await transaction.CommitAsync(); 
            }
        }

        public async Task Remove(Company product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.DeleteAsync(product); 
                await transaction.CommitAsync(); 
            }
        } 
        
        public async Task<Company> GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Company>(productId);
            }
        }
        public async Task<Company> GetBySubquery()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                var subquery = QueryOver.Of<AddressCompany>()
                    .Where(c => c.CreationDate > new DateTime(2000, 1, 1))
                    .Select(c => c.Company.Id);

                var query = await session.QueryOver<Company>()
                    .WithSubquery
                    .WhereProperty(c => c.Id)
                    .In(subquery)
                    .SingleOrDefaultAsync();

                return query;
            }
        }


        
    }
}
