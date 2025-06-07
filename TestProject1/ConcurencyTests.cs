using Demo.Domain.AggregatesModel.Company2Aggregate;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using Demo.Domain.AggregatesModel.ProductAggregate;
using Demo.Infrastructure;
using Demo.Infrastructure.Database;
using Demo.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Demo.UnitTests
{
    [TestClass]
    public sealed class ConcurencyTests
    {
        private readonly INHibernateHelper _nHibernateHelper;
        private readonly IAddress2Repository _address2Repository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompany2Repository _company2Repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;
        public ConcurencyTests()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.Test.json", optional: false);
            var _config = builder.Build();
            _nHibernateHelper = new NHibernateHelper(_config);
            _address2Repository = new Address2Repository(_nHibernateHelper);
            _addressRepository = new AddressRepository(_nHibernateHelper);
            _company2Repository = new Company2Repository(_nHibernateHelper);
            _companyRepository = new CompanyRepository(_nHibernateHelper);
            _productRepository = new ProductRepository(_nHibernateHelper);
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            // This method is called once for the test class, before any tests of the class are run.
        }

        [TestMethod]
        public async Task UpdateProduct()
        {
            // Arrange:
            var product = new Product
            {
                Name = "Sample Product",
                Category = "Sample Category",
                Discontinued = false
            };

            // Act:

            object result = await _productRepository.Add(product);
            Product retrievedProduct = await _productRepository.GetById((int)result);
            retrievedProduct.Name = "Updated Product Name"; // Simulate a change
            await _productRepository.Update(retrievedProduct);
            Product updated = await _productRepository.GetById((int)result);

            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(updated.Version, 2);
        }
    }
}
