using FluentNHibernate.Mapping;
using Migrations.Conventions;

namespace WebApplication1.Domain
{
    public class Company
    {
        public virtual int Id { get; set; }
        public virtual IList<AddressCompany> Addresses { get; set; }    
    }

    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            //Table("Company")/*;*/
            Id(x => x.Id).GeneratedBy.Increment().Not.Nullable();
            HasMany(x => x.Addresses)
                .Table(LowercaseTableNameConvention.TablePrefix + "AddressCompany")
                .KeyColumn("CompanyId")
                .Inverse()
                .Cascade.All();
        }
    }
}
