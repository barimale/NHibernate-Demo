using Microsoft.Extensions.Configuration;
using NHibernate;
using WebApplication1.Domain;
using WebApplication1.Repositories.DbContext;

namespace TestProject1
{
    [TestClass]
    public sealed class Test1
    {
        private readonly INHibernateHelper _nHibernateHelper;

        public Test1()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.Development.json", optional: false);
            var _config = builder.Build();
            _nHibernateHelper = new NHibernateHelper(_config);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // This method is called once for the test class, before any tests of the class are run.
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Sample Product",
                Category = "Sample Category",
                Discontinued = false
            };

            // Act:
            object result;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                result = session.Save(product);
                transaction.Commit();
            }
            // Assert: 
            Assert.IsNotNull(result);
        }
    }
}
