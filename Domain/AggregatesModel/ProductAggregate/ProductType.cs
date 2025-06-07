using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    public class ProductType: Entity
    {
        public virtual string Description { get; set; }
    }
}
