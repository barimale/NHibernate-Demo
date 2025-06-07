using Demo.Infrastructure.Database;
using FluentNHibernate.Testing;
using Microsoft.Extensions.Configuration;
using NHibernate;
using Demo.Infrastructure;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Demo.Domain.AggregatesModel.Company2Aggregate;

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
        public async Task AddProduct()
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
            Product retrievedProduct;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                result = session.Save(product);
                transaction.Commit();
            }
            using (var session = _nHibernateHelper.OpenStatelessSesion())
            {
                retrievedProduct = session.Get<Product>(result);
            }
            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);

        }

        [TestMethod]
        public async Task AddAddress()
        {
            // Arrange:
            var product = new Address
            {
                City = "Sample Product",
                Country = "Sample Category",
                Street = "dsa",
                State = "state",
                ZipCode = "zipcode",
                Phone = "phone"
            };

            // Act:
            object result;
            Address retrievedProduct;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                result = session.Save(product);
                transaction.Commit();
            }
            using (var session = _nHibernateHelper.OpenStatelessSesion())
            {
                retrievedProduct = session.Get<Address>(result);
            }
            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);

        }

        [TestMethod]
        public async Task AddCompany()
        {
            // Arrange:
            var product = new Company
            {
                Foo = "Sample Company"
            };

            // Act:
            object result;
            Company retrievedProduct;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                result = session.Save(product);
                transaction.Commit();
            }
            using (var session = _nHibernateHelper.OpenStatelessSesion())
            {
                retrievedProduct = session.Get<Company>(result);
            }
            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);

        }

        [TestMethod]
        public void Product_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
            new PersistenceSpecification<Product>(session)
              .CheckProperty(p => p.Name, "Product Name")
              .CheckProperty(p => p.Category, "Category Name")
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
                    .VerifyTheMappings();
        }

        [TestMethod]
        public void Company2_persistence_test()
        {
            using (ISession session = _nHibernateHelper.OpenSession())
                new PersistenceSpecification<Company2>(session)
                    .CheckProperty(p => p.Foo, "Foo Name")
                    .VerifyTheMappings();
        }

        [TestMethod]
        public async Task AddOneToManyTwice()
        {
            // Arrange:
                var address = new Address
            {
                City = "Sample Product",
                Country = "Sample Category",
                Street = "dsa",
                State = "state",
                ZipCode = "zipcode",
                Phone = "phone"
            };
            var company = new Company { Foo = "city" };

            // Act:
            CompanyAddress result;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                address.Companies.Add(company);
                company.Addresses.Add(address);

                // Save entities
                var addresId = session.Save(address);
                var companyId = session.Save(company);
                var caid = session.Save(new CompanyAddress()
                {
                    Description = "aaa",
                    Company = company,
                    Address = address,
                    CreationDate = DateTime.Now,
                });
                transaction.Commit();
                result = session.Get<CompanyAddress>(caid);

            }
            // Assert: 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task AddManyToMany()
        {
            // Arrange:
            var address = new Address2
            {
                City = "Sample Product",
                Country = "Sample Category",
                Street = "dsa",
                State = "state",
                ZipCode = "zipcode",
                Phone = "phone"
            };
            var company = new Company2 { Foo = "city" };

            // Act:
            CompanyAddress2 result;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                address.Companies.Add(company);
                company.Addresses.Add(address);

                // Save entities
                var addresId = session.Save(address);
                var companyId = session.Save(company);
               
                transaction.Commit();
                result = session.Get<CompanyAddress2>(1);

            }
            // Assert: 
            Assert.IsNotNull(result);
        }
    }
}
