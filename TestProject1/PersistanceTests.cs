using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure;
using Demo.Infrastructure.Database;
using FluentNHibernate.Testing;
using Microsoft.Extensions.Configuration;
using NHibernate;

namespace Demo.UnitTests
{
    [TestClass]
    public sealed class PersistanceTests
    {
        private readonly INHibernateHelper _nHibernateHelper;

        public PersistanceTests()
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
        public void Product_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            new PersistenceSpecification<Product>(session)
              .CheckProperty(p => p.Name, "Product Name")
              .CheckProperty(p => p.Category, "Category Name")
              .CheckProperty(p => p.Discontinued, true)
              //.CheckProperty(p => p.Type, new ProductType() { Description = " booo"})
              .VerifyTheMappings();
        }

        [TestMethod]
        public void Address_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
                new PersistenceSpecification<Address>(session)
                  .CheckProperty(p => p.Street, "Street Name")
                  .VerifyTheMappings();
        }


        [TestMethod]
        public void Address2_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
                new PersistenceSpecification<Address2>(session)
                    .CheckProperty(p => p.Street, "Street Name")
                    .CheckProperty(p => p.ZipCode, "Street Name")
                    .CheckProperty(p => p.City, "Street Name")
                    .CheckProperty(p => p.Country, "Street Name")
                    .CheckProperty(p => p.Phone, "Street Name")
                    .CheckProperty(p => p.State, "Street Name")
                    .VerifyTheMappings();
        }

        [TestMethod]
        public void Company_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
                new PersistenceSpecification<Company>(session)
                    .CheckProperty(p => p.Foo, "Foo Name")
                                      .CheckList(p => p.Addresses, (new Address[] { new Address { City = "bar" } }).ToList())

                    .VerifyTheMappings();
        }

        [TestMethod]
        public void Company2_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
                new PersistenceSpecification<Company2>(session)
                    .CheckProperty(p => p.Foo, "Foo Name")
                    .CheckList(
                         c => c.Addresses,
                         new List<Address2> {
                             new Address2 { ZipCode = "A001"},
                             new Address2 { ZipCode = "A002" }
                         })
                    .VerifyTheMappings();
        }
    }
}
