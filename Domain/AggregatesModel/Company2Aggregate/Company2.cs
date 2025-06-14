using Demo.Domain.Abstraction;
using NHibernate.Envers.Configuration.Attributes;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    [Audited]
    public class Company2: Entity<int>, IAggregateRoot
    {
        public virtual string Foo { get; set; }

        [Audited(TargetAuditMode = RelationTargetAuditMode.NotAudited)]
        public virtual IList<Address2> Addresses { get; set; } = new List<Address2>();
    }
}
