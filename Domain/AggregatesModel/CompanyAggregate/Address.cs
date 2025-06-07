using Demo.Domain.Abstraction;
using Demo.Migrations.Conventions;
using FluentNHibernate.Mapping;

namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public class Address: Entity
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual IList<Company> Companies { get; set; } = new List<Company>();

    }

    public class AddressMap: ClassMap<Address>
    {
        public AddressMap()
        {
            //Table("Address")/*;*/
            Id(x => x.Id).GeneratedBy.TriggerIdentity();
            Map(x => x.Street).Length(100).Nullable();
            Map(x => x.City).Length(50).Nullable();
            Map(x => x.State).Length(50).Nullable();
            Map(x => x.ZipCode).Length(20).Nullable();
            Map(x => x.Country).Length(50).Nullable();
            Map(x => x.Phone).Length(50).Nullable();
            // Define the relationship with AddressCompany
            HasMany(x => x.Companies)
                .Table(LowercaseTableNameConvention.TablePrefix + "CompanyAddress")
                .KeyColumn("AddressId")
                .Inverse()
                .Cascade.All();
        }
    }
}
