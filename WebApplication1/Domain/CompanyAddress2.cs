using FluentNHibernate.Mapping;

namespace WebApplication1.Domain
{
    public class CompanyAddress2
    {
        public virtual int Id { get; set; }
        // the relation to both sides
        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }

    }

    public class AddressCompany2Map : ClassMap<CompanyAddress2>
    {
        public AddressCompany2Map()
        {
            //Table("AddressCompany")/*;*/
            Id(x => x.Id).GeneratedBy.TriggerIdentity();
            Id(x => x.Address.Id).GeneratedBy.TriggerIdentity().Not.Nullable();
            Id(x => x.Company.Id).GeneratedBy.TriggerIdentity().Not.Nullable();
            
            References(x => x.Address)
                .Column("AddressId")
                .Not.Nullable()
                .Cascade.None(); // Adjust cascade as needed
            References(x => x.Company)
                .Column("CompanyId")
                .Not.Nullable()
                .Cascade.None(); // Adjust cascade as needed
        }
    }
}
