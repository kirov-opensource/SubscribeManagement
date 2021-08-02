using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using SubscribeManagement.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly SQLiteConnection _connection;
        private readonly IMapper _mapper;

        public ServerController(SQLiteConnection connection, IMapper mapper)
        {
            _connection = connection;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<ServerViewModel>> Query([FromQuery] SearchServerModel serverSearchModel)
        {
            var result = _connection.Table<Server>().ToList();
            return result.Select(c => new ServerViewModel
            {
                Id = c.Id,
                Address = c.Address,
                Port = c.Port
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServerModel createServerModel)
        {
            var server = _mapper.Map<Server>(createServerModel);
            _connection.Insert(server);
            return NoContent();
        }

        [HttpPut("{dataId:int}")]
        public async Task<IActionResult> Update(int dataId, [FromBody] UpdateServerModel createServerModel)
        {
            var data = _connection.Table<Server>().Where(c => c.DataId == dataId).FirstOrDefault();
            if (data == null)
                return NotFound("数据未找到");

            var server = _mapper.Map<Server>(createServerModel);
            _connection.Update(server);
            return NoContent();
        }

    }
}
