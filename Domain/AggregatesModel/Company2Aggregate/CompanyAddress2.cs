using Demo.Domain.Abstraction;
using Demo.Domain.AggregatesModel.CompanyAggregate;
using NHibernate.Envers.Configuration.Attributes;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    [Audited]
    public class CompanyAddress2: Entity<int>
    {
        // the relation to both sides
        public virtual Address2 Address { get; set; }
        public virtual Company2 Company { get; set; }

    }
}
