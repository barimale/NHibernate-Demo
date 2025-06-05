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
            _nHibernateHelper = new InMemoryNHibernateHelper();
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
        public async Task AddManyToMany()
        {
            // Arrange:
            var address = new Address { Street = "John Doe" };
            var company = new Company { };

            // Act:
            AddressCompany result;
            using (ISession session = _nHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                address.Companies.Add(company);
                company.Addresses.Add(address);

                // Save entities
                session.Save(address);
                session.Save(company);

                transaction.Commit();
                result = session.Get<AddressCompany>(1);

            }
            // Assert: 
            Assert.IsNotNull(result);

        }
    }
}
