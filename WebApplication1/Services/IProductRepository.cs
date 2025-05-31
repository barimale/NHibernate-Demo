using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        Product GetById(int productId);
        Product GetByName(string name);
    }

    public class ProductRepository : IProductRepository 
    {
        private readonly INHibernateHelper _nHibernateHelper;
        public ProductRepository(INHibernateHelper nHibernateHelper)
        {
            _nHibernateHelper = nHibernateHelper;
        }

        public void Add(Product product)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(product); 
                transaction.Commit();
            }
        }
        
        public void Update(Product product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                session.Update(product); 
                transaction.Commit(); 
            }
        }

        public void Remove(Product product) 
        {
            using (ISession session = _nHibernateHelper.OpenSession()) 
            using (ITransaction transaction = session.BeginTransaction()) 
            { 
                session.Delete(product); 
                transaction.Commit(); 
            }
        } 
        
        public Product GetById(int productId) 
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                return session.Get<Product>(productId);
            }
        }

        public Product GetByName(string name)
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                // WIP modify to LINQ
                Product product = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Product>();
                
                return product;
            }
        }
    }
}
