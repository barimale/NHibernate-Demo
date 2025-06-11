using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    [Serializable]
    public class ProductType: Entity<int>, IAggregateRoot
    {
        public virtual string Description { get; set; }
    }
}
