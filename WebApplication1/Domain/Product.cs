
using FluentNHibernate.Mapping;

namespace WebApplication1.Domain
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }
    }

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
        }
    }
}
