using AngularNgxDataTableBackend.Application.Features.Employees.Queries.GetEmployees;
using AngularNgxDataTableBackend.Application.Parameters;
using AngularNgxDataTableBackend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularNgxDataTableBackend.Application.Interfaces.Repositories
{
    public interface IEmployeeRepositoryAsync : IGenericRepositoryAsync<Employee>
    {
        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedEmployeeReponseAsync(GetEmployeesQuery requestParameter);
    }
}
