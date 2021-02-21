﻿using AngularNgxDataTableBackend.Application.Features.Positions.Commands.CreatePosition;
using AngularNgxDataTableBackend.Application.Features.Positions.Commands.DeletePositionById;
using AngularNgxDataTableBackend.Application.Features.Positions.Commands.UpdatePosition;
using AngularNgxDataTableBackend.Application.Features.Positions.Queries.GetPositionById;
using AngularNgxDataTableBackend.Application.Features.Positions.Queries.GetPositions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularNgxDataTableBackend.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class PositionsController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPositionsQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// GET api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPositionByIdQuery { Id = id }));
        }

        /// <summary>
        /// POST api/controller
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> Post(CreatePositionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Bulk insert fake data by specifying number of rows
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMock")]
        // [Authorize]
        public async Task<IActionResult> AddMock(InsertMockPositionCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Support Ngx-DataTables https://medium.com/scrum-and-coke/angular-11-pagination-of-zillion-rows-45d8533538c0
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Paged")]
        // [Authorize]
        public async Task<IActionResult> Paged(PagedPositionsQuery command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// PUT api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdatePositionCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// DELETE api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePositionByIdCommand { Id = id }));
        }
    }
}