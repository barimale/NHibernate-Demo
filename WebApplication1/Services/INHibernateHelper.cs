namespace NHibernateTestBlog
{
    public interface INHibernateHelper
    {
         NHibernate.ISession OpenSession();
    }
}