using Demo.Domain.Abstraction;
using Demo.Domain.AggregatesModel.ProductAggregate;
using FluentValidation;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public class Address2: Entity<int>
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string Phone { get; set; }
        public virtual IList<Company2> Companies { get; set; } = new List<Company2>();

    }
}
