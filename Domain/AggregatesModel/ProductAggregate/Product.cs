using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public class Product: Entity<int>
    {
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}
