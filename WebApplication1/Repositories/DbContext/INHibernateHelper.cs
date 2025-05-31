namespace WebApplication1.Repositories.DbContext
{
    public interface INHibernateHelper
    {
         NHibernate.ISession OpenSession();
    }
}