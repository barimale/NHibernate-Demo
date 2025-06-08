using NHibernate;
using NLog;
using NHibernate.SqlCommand;

namespace Demo.Infrastructure.Database.Interceptors
{
    public class NHibernateInterceptor : EmptyInterceptor
    {
        private static Logger logger = LogManager.CreateNullLogger(); // GetLogger("NHibernateInterceptor");

        public override SqlString OnPrepareStatement(SqlString sql)
        {
            logger.Debug($"Executing SQL: {sql}");
            return base.OnPrepareStatement(sql);
        }
    }
}
