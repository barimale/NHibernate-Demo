using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public class ProductType: Entity<int>
    {
        public virtual string Description { get; set; }
    }
}
