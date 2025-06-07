using Demo.Domain.AggregatesModel.ProductAggregate;
using FluentNHibernate.Mapping;

namespace Demo.Infrastructure.EntityConfigurations
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            //Table("Product")/*;*/
#if TEST
            Id(u => u.Id).GeneratedBy.Identity();
#else
            Id(u => u.Id).GeneratedBy.TriggerIdentity();
#endif
            Map(u => u.Name).Length(50).Nullable();
            Map(u => u.Category).Length(50).Nullable();
            Map(u => u.Discontinued).Nullable();
            Version(u => u.Version).Nullable();
            //OptimisticLock.Version();
        }
    }
}
