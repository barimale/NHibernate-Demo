using Demo.Domain.AggregatesModel.Company2Aggregate;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.EntityConfigurations
{
    public class AuditRevisionEntityMap : ClassMap<AuditRevisionEntity>
    {
        public AuditRevisionEntityMap()
        {
            Table("REVINFO");
            Id(x => x.Id).GeneratedBy.TriggerIdentity();
            Map(x => x.RevisionDate);
        }
    }
}
