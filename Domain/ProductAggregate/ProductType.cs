using FluentNHibernate.Data;
using FluentNHibernate.Mapping;

namespace Demo.Domain.ProductAggregate
{
    public class ProductType
    {
        public virtual int Id { get; set; }
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
