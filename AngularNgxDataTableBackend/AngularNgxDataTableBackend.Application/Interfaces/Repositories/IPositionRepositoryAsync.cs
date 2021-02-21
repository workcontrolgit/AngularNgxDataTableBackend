using AngularNgxDataTableBackend.Application.Features.Positions.Queries.GetPositions;
using AngularNgxDataTableBackend.Application.Parameters;
using AngularNgxDataTableBackend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularNgxDataTableBackend.Application.Interfaces.Repositories
{
    public interface IPositionRepositoryAsync : IGenericRepositoryAsync<Position>
    {
        Task<bool> IsUniquePositionNumberAsync(string positionNumber);

        Task<bool> SeedDataAsync(int rowCount);

        Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedPositionReponseAsync(GetPositionsQuery requestParameters);
    }
}