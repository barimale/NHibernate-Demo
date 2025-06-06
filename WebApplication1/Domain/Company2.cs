using FluentNHibernate.Mapping;
using Migrations.Conventions;

namespace WebApplication1.Domain
{
    public class Company2
    {
        public virtual int Id { get; set; }
        public virtual string Foo { get; set; }

        public virtual IList<Address2> Addresses { get; set; } = new List<Address2>();
    }

    public class Company2Map : ClassMap<Company2>
    {
        public Company2Map()
        {
            //Table("Company")/*;*/
            Id(x => x.Id).GeneratedBy.TriggerIdentity();
            Map(x => x.Foo).Length(50).Nullable();
            HasManyToMany(x => x.Addresses)
                .Table(LowercaseTableNameConvention.TablePrefix + "CompanyAddress2")
                .ParentKeyColumn("CompanyId")
                .ChildKeyColumn("AddressId")
                .Inverse()
                .Cascade.All();
        }
    }
}
