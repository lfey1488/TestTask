using FluentNHibernate.Mapping;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Mappings
{
    public class EmployeeMap : ClassMap<EmployeeEntity>
    {
        public EmployeeMap()
        {
            Table("Employees");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.FullName).Not.Nullable();
            Map(x => x.Position).CustomType<int>().Not.Nullable();
            Map(x => x.BirthDate).Not.Nullable();
        }
    }
}