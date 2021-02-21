using AngularNgxDataTableBackend.Application.Features.Employees.Queries.GetEmployees;
using AngularNgxDataTableBackend.Application.Features.Positions.Commands.CreatePosition;
using AngularNgxDataTableBackend.Application.Features.Positions.Queries.GetPositions;
using AngularNgxDataTableBackend.Domain.Entities;
using AutoMapper;

namespace AngularNgxDataTableBackend.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Position, GetPositionsViewModel>().ReverseMap();
            CreateMap<Employee, GetEmployeesViewModel>().ReverseMap();
            CreateMap<CreatePositionCommand, Position>();
        }
    }
}