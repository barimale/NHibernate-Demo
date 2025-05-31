using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace WebApplication1.Conventions
{
    public class LowercaseTableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table("tbl_" + instance.EntityType.Name);
        }
    }
}
