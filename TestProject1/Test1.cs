using Microsoft.Extensions.Configuration;
using NHibernate;
using WebApplication1.Domain;
using WebApplication1.Repositories.DbContext;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

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
                result = await session.SaveAsync(product);
                await transaction.CommitAsync();
            }
            // Assert: 
            Assert.IsNotNull(result);
        }
    }
}
