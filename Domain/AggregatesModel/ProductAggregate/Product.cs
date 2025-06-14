using Demo.Domain.Abstraction;
using FluentValidation;
using NHibernate.Envers.Configuration.Attributes;

namespace Demo.Domain.AggregatesModel.ProductAggregate
{
    [Serializable]
    [Audited]
    public class Product: Entity<int>, IAggregateRoot
    {
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual bool Discontinued { get; set; }
        public virtual ProductType Type { get; set; }   
    }
}
