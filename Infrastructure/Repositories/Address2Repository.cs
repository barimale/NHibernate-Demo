using Demo.Domain.Company2Aggregate;
using Demo.Infrastructure.Database;
using NHibernate;
using ISession = NHibernate.ISession;

namespace Demo.Infrastructure.Repositories
{
    public class Address2Repository : IAddress2Repository
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public Address2Repository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public void AssingAddressToCompany(int addressId, int companyId)
        {
            using (var session = _nHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var address = session.Get<Address2>(addressId);
                var company = session.Get<Company2>(companyId);

                if (address != null && company != null)
                {
                    address.Companies.Add(company);
                    company.Addresses.Add(address);

                    session.Update(address);
                    session.Update(company);
                    transaction.Commit();
                }
            }
        }

        public async Task<int> Add(Address2 product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                var result = await session.SaveAsync(product);
                await transaction.CommitAsync();

                return (int)result;
            }
        }
        
        public async Task Update(Address2 product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.UpdateAsync(product); 
                await transaction.CommitAsync(); 
            }
        }

        public async Task Remove(Address2 product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                await session.DeleteAsync(product); 
                await transaction.CommitAsync(); 
            }
        } 
        
        public async Task<Address2> GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return await session.GetAsync<Address2>(productId);
            }
        }

        public async Task<Address2> GetByName(string name)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                // Corrected to use QueryOver for LINQ-like querying
                Address2 product = await session.QueryOver<Address2>()
                    .Where(p => p.Country == name)
                    .Fetch(SelectMode.Fetch, x => x.Companies) // Eagerly fetch related Companies
                    .SingleOrDefaultAsync();

                return product;
            }
        }
    }
}
