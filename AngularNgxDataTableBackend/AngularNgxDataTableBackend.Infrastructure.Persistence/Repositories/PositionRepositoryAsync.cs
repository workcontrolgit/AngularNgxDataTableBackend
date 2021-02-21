﻿using AngularNgxDataTableBackend.Application.Features.Positions.Queries.GetPositions;
using AngularNgxDataTableBackend.Application.Interfaces;
using AngularNgxDataTableBackend.Application.Interfaces.Repositories;
using AngularNgxDataTableBackend.Application.Parameters;
using AngularNgxDataTableBackend.Domain.Entities;
using AngularNgxDataTableBackend.Infrastructure.Persistence.Contexts;
using AngularNgxDataTableBackend.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AngularNgxDataTableBackend.Infrastructure.Persistence.Repositories
{
    public class PositionRepositoryAsync : GenericRepositoryAsync<Position>, IPositionRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Position> _positions;
        private IDataShapeHelper<Position> _dataShaper;
        private readonly IMockService _mockData;

        public PositionRepositoryAsync(ApplicationDbContext dbContext,
            IDataShapeHelper<Position> dataShaper, IMockService mockData) : base(dbContext)
        {
            _dbContext = dbContext;
            _positions = dbContext.Set<Position>();
            _dataShaper = dataShaper;
            _mockData = mockData;
        }

        public async Task<bool> IsUniquePositionNumberAsync(string positionNumber)
        {
            return await _positions
                .AllAsync(p => p.PositionNumber != positionNumber);
        }

        public async Task<bool> SeedDataAsync(int rowCount)
        {
            foreach (Position position in _mockData.GetPositions(rowCount))
            {
                await this.AddAsync(position);
            }
            return true;
        }

        public async Task<(IEnumerable<Entity> data, RecordsCount recordsCount)> GetPagedPositionReponseAsync(GetPositionsQuery requestParameter)
        {
            var positionNumber = requestParameter.PositionNumber;
            var positionTitle = requestParameter.PositionTitle;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            // Setup IQueryable
            var result = _positions
                .AsNoTracking()
                .AsExpandable();

            // Count records total
            recordsTotal = await result.CountAsync();

            // filter data
            FilterByColumn(ref result, positionNumber, positionTitle);

            // Count records after filter
            recordsFiltered = await result.CountAsync();

            //set Record counts
            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            // set order by
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                result = result.OrderBy(orderBy);
            }

            // select columns
            if (!string.IsNullOrWhiteSpace(fields))
            {
                result = result.Select<Position>("new(" + fields + ")");
            }
            // paging
            result = result
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // retrieve data to list
            var resultData = await result.ToListAsync();
            // shape data
            var shapeData = _dataShaper.ShapeData(resultData, fields);

            return (shapeData, recordsCount);
        }

        private void FilterByColumn(ref IQueryable<Position> positions, string positionNumber, string positionTitle)
        {
            if (!positions.Any())
                return;

            if (string.IsNullOrEmpty(positionTitle) && string.IsNullOrEmpty(positionNumber))
                return;

            var predicate = PredicateBuilder.New<Position>();

            if (!string.IsNullOrEmpty(positionNumber))
                predicate = predicate.Or(p => p.PositionNumber.Contains(positionNumber.Trim()));

            if (!string.IsNullOrEmpty(positionTitle))
                predicate = predicate.Or(p => p.PositionTitle.Contains(positionTitle.Trim()));

            positions = positions.Where(predicate);
        }
    }
}
