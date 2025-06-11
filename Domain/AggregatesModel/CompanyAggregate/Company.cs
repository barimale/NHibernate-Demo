using Demo.Domain.Abstraction;

namespace Demo.Domain.AggregatesModel.CompanyAggregate
{
    public class Company: Entity<int>, IAggregateRoot
    {
        public virtual string Foo { get; set; }

        public virtual IList<Address> Addresses { get; set; } = new List<Address>();
    }
}
