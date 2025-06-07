using Demo.Domain.Abstraction;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using FluentNHibernate.Mapping;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public class CompanyAddress2: Entity
    {
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
