using FluentNHibernate.Mapping;
using TeskTask.Core.Models;

namespace TestTask.Persistence.Mappings
{
    public class ContractorMap : ClassMap<Contractor>
    {
        public ContractorMap()
        {
            Table("Contractors");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Inn).Not.Nullable();
            References(x => x.CuratorId).Column("CuratorId").Not.Nullable();
        }
    }
}
