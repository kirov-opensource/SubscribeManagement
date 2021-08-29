using Microsoft.AspNetCore.Mvc;
using SubscribeManagement.WebAPI.Models;
using SubscribeManagement.WebAPI.Models.Connection;
using SubscribeManagement.WebAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private readonly ConnectionService _connectionService;

        public ConnectionController(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }


        [HttpGet]
        public async Task<IActionResult> PageSearch([FromQuery] PageSearchModel<SearchConnectionModel> pageSearchModel)
        {
            var result = await _connectionService.PageSearch(pageSearchModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConnectionModel createModel)
        {
            await _connectionService.Create(createModel);
            return NoContent();
        }

        [ProducesResponseType(typeof(string), 200)]
        [SwaggerResponse(200, "正常,返回URI")]
        [ProducesResponseType(typeof(MessageModel), 200)]
        [SwaggerResponse(400, "异常,返回错误模型")]
        [HttpGet("{id:long}/uri")]
        public async Task<IActionResult> GetURI(long id)
        {
            var result = await _connectionService.GetURI(id);
            return Ok(result);
        }
    }
}
