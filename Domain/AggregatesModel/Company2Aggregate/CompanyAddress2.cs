using Demo.Domain.Abstraction;
using Demo.Domain.AggregatesModel.CompanyAggregate;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public class CompanyAddress2: Entity<int>
    {
        // the relation to both sides
        public virtual Address Address { get; set; }
        public virtual Company Company { get; set; }

    }
}
