using AutoMapper;
using TeskTask.Core.Models;
using TestTask.Persistence.Entities;

namespace TestTask.Persistence.Configurations
{
    public class PersistenceProfile : Profile
    {
        public PersistenceProfile()
        {
            CreateMap<ContractorEntity, Contractor>()
                .ForMember(dest => dest.CuratorId, opt => opt.MapFrom(src => src.Curator.Id));
            CreateMap<Contractor, ContractorEntity>();

            CreateMap<EmployeeEntity, Employee>();
            CreateMap<Employee, EmployeeEntity>();

            CreateMap<OrderEntity, Order>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(dest => dest.ContractorId, opt => opt.MapFrom(src => src.Contractor.Id));
            CreateMap<Order, OrderEntity>();
        }
    }
}
