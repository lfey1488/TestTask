using FluentNHibernate.Mapping;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Mappings
{
    public class OrderMap : ClassMap<OrderEntity>
    {
        public OrderMap()
        {
            Table("Orders");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Date).Not.Nullable();
            Map(x => x.Amount).Not.Nullable();
            References(x => x.Employee).Column("EmployeeId").Not.Nullable();
            References(x => x.Contractor).Column("ContractorId").Not.Nullable();
        }
    }
}