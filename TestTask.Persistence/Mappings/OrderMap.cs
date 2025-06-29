using FluentNHibernate.Mapping;
using TeskTask.Core.Models;

namespace TestTask.Persistence.Mappings
{
    public class OrderMap : ClassMap<Order>
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
