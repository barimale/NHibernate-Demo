using NHibernate.Envers;
using NHibernate.Envers.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Domain.AggregatesModel.Company2Aggregate
{
    [RevisionEntity]
    [Table("REVINFO")]
    public class AuditRevisionEntity : DefaultRevisionEntity
    {
        //[RevisionNumber]
        //public virtual int REV { get; set; }

        ////[Column("REVTSTMP")]
        //[RevisionTimestamp]
        //public virtual DateTime REVTSTMP { get; set; }

        //[Column("REVTSTMP")]
        //public DateTime Timestamp { get; set; }
    }
}
