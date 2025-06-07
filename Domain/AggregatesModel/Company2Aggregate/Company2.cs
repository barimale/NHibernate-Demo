using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    public class Company2: Entity<int>
    {
        public virtual string Foo { get; set; }

        public virtual IList<Address2> Addresses { get; set; } = new List<Address2>();
    }
}
