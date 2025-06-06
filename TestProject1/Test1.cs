using FluentNHibernate.Testing;
using Microsoft.AspNetCore.Routing;
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
        public async Task AddManyToMany()
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
    }
}
