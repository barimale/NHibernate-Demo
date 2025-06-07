using Demo.Domain.Abstraction;
using Demo.Migrations.Conventions;
using FluentNHibernate.Mapping;

namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public class Company: Entity
    {
        public virtual string Foo { get; set; }

        public virtual IList<Address> Addresses { get; set; } = new List<Address>();
    }

    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            //Table("Company")/*;*/
            Id(x => x.Id).GeneratedBy.TriggerIdentity();
            Map(x => x.Foo).Length(50).Nullable();
            HasMany(x => x.Addresses)
                .Table(LowercaseTableNameConvention.TablePrefix + "CompanyAddress")
                .KeyColumn("CompanyId")
                .Inverse()
                .Cascade.All();
        }
    }
}
