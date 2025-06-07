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
    public sealed class RepositoryTests
    {
        private readonly INHibernateHelper _nHibernateHelper;
        private readonly IAddress2Repository _address2Repository;
        private readonly IAddressRepository _addressRepository;
        private readonly ICompany2Repository _company2Repository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IProductRepository _productRepository;
        public RepositoryTests()
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

            object result = await _productRepository.Add(product);
            Product retrievedProduct = await _productRepository.GetById((int)result);
            
            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);
        }

        [TestMethod]
        public async Task AddAddress()
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

            // Act:
            var result = await _addressRepository.Add(address);
            Address retrievedProduct = await _addressRepository.GetById((int)result);

            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);
        }

        [TestMethod]
        public async Task AddCompany()
        {
            // Arrange:
            var company = new Company
            {
                Foo = "Sample Company"
            };

            // Act:
            var result = await _companyRepository.Add(company);
            Company retrievedProduct = await _companyRepository.GetById((int)result);

            // Assert: 
            Assert.IsNotNull(result);
            Assert.IsNotNull(retrievedProduct);

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
            var resultAddress = await _addressRepository.Add(address);
            var resultCompany = await _companyRepository.Add(company);

            var relationId = _addressRepository.AssingAddressToCompany(resultAddress, resultCompany, "description");
            Company retrievedProduct = await _companyRepository.GetById((int)resultCompany);

            // Assert: 
            Assert.IsNotNull(relationId);
            Assert.IsNotNull(retrievedProduct);
            Assert.IsNotNull(retrievedProduct.Addresses);
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
            var resultAddress = await _address2Repository.Add(address);
            var resultCompany = await _company2Repository.Add(company);

            _address2Repository.AssingAddressToCompany(resultAddress, resultCompany);
            Company2 retrievedProduct = await _company2Repository.GetById((int)resultCompany);

            // Assert: 
            Assert.IsNotNull(retrievedProduct);
            Assert.IsNotNull(retrievedProduct.Addresses);
        }
    }
}
