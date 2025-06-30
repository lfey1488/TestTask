using FluentNHibernate.Mapping;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Mappings
{
    public class ContractorMap : ClassMap<ContractorEntity>
    {
        public ContractorMap()
        {
            Table("Contractors");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Inn).Not.Nullable();
            References(x => x.Curator).Column("CuratorId").Not.Nullable();
        }
    }
}
