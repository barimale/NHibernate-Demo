using FluentNHibernate.Mapping;

namespace WebApplication1.Domain
{
    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual IList<AddressCompany> Companies { get; set; }

    }

    public class AddressMap: ClassMap<Address>
    {
        public AddressMap()
        {
            //Table("Address")/*;*/
            Id(x => x.Id).GeneratedBy.Increment().Not.Nullable();
            Map(x => x.Street).Length(100).Nullable();
            Map(x => x.City).Length(50).Nullable();
            Map(x => x.State).Length(50).Nullable();
            Map(x => x.ZipCode).Length(20).Nullable();
            Map(x => x.Country).Length(50).Nullable();
            // Define the relationship with AddressCompany
            HasMany(x => x.Companies)
                .Table("AddressCompany")
                .KeyColumn("AddressId")
                .Inverse()
                .Cascade.All();
        }
    }
}
