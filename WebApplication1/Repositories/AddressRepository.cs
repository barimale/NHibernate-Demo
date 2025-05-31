using NHibernate;
using WebApplication1.Domain;
using WebApplication1.Repositories.DbContext;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public interface IAddressRepository
    {
        Task<int> Add(Address product);
        Task Update(Address product);
        Task Remove(Address product);
        Task<Address> GetById(int addressId);
        Task<Address> GetByName(string name);
    }

    public class AddressRepository : IAddressRepository
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public AddressRepository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public async Task<int> Add(Address product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var result = await session.SaveAsync(product);
                await transaction.CommitAsync();

                return (int)result;
            }
        }
        
        public async Task Update(Address product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.UpdateAsync(product); 
                await transaction.CommitAsync(); 
            }
        }

        public async Task Remove(Address product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.DeleteAsync(product); 
                await transaction.CommitAsync(); 
            }
        } 
        
        public async Task<Address> GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Address>(productId);
            }
        }

        public async Task<Address> GetByName(string name)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                // Corrected to use QueryOver for LINQ-like querying
                Address product = await session.QueryOver<Address>()
                    .Where(p => p.Country == name)
                    .SingleOrDefaultAsync();

                return product;
            }
        }

    //    var subquery = QueryOver.Of<AddressCompany>()
    //.Where(c => c.CreationDate > new DateTime(2000, 1, 1))
    //.Select(c => c.Company.ID);

    //    var query = session.QueryOver<Company>()
    //        .WithSubquery
    //        .WhereProperty(c => c.ID)
    //        .In(subquery)
    }
}
