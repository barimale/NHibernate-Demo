using Demo.Domain.Abstraction;
using FluentNHibernate.Mapping;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public class ProductType: Entity
    {
        public virtual string Description { get; set; }
    }
    public class ProductTypeMap : ClassMap<ProductType>
    {
        public ProductTypeMap()
        {
            //Table("ProductType");
            Id(u => u.Id).GeneratedBy.Increment().Not.Nullable();
            Map(u => u.Description).Length(50).Nullable();
        }
    }
}
