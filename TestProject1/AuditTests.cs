using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure;
using Demo.Infrastructure.Database;
using FluentNHibernate.Testing;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Envers;
using System;

namespace Demo.UnitTests
{
    [TestClass]
    public sealed class AuditTests
    {
        private readonly INHibernateHelper _nHibernateHelper;

        public AuditTests()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.Test.json", optional: false);
            var _config = builder.Build();
            _nHibernateHelper = new NHibernateHelper(_config);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // This method is called once for the test class, before any tests of the class are run.
        }

        [TestMethod]
        public void Audit_dummy_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            {
                // Arrange:
                var result = new PersistenceSpecification<Product>(session)
                  .CheckProperty(p => p.Name, "Product Name")
                  .CheckProperty(p => p.Category, "Category Name")
                  .CheckProperty(p => p.Discontinued, true)
                  .CheckProperty(p => p.Type, new ProductType() { Description = " booo" })
                  .VerifyTheMappings();

                var reader = AuditReaderFactory.Get(session);
                var auditedProduct = reader.Find<Product>(result.Id, 41);
                Assert.AreEqual(auditedProduct.Name, "Product Name");
            }
        }
    }
}
