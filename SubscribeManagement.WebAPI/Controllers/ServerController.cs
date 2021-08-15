using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLite;
using SubscribeManagement.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
            var result = _connection.Table<Server_bak>().ToList();
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
            var server = _mapper.Map<Server_bak>(createServerModel);
            _connection.Insert(server);
            return NoContent();
        }

        [HttpPut("{dataId:int}")]
        public async Task<IActionResult> Update(int dataId, [FromBody] UpdateServerModel createServerModel)
        {
            var data = _connection.Table<Server_bak>().Where(c => c.DataId == dataId).FirstOrDefault();
            if (data == null)
                return NotFound("数据未找到");

            var server = _mapper.Map<Server_bak>(createServerModel);
            _connection.Update(server);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetShareLink(int dataId)
        {
            var data = _connection.Table<Server_bak>().Where(c => c.DataId == dataId).FirstOrDefault();
            if (data == null)
                return NotFound("数据未找到");
            string result = string.Empty;
            string remark = string.Empty;
            string url = string.Empty;
            string query = string.Empty;

            //switch (data.ConfigType)
            //{
            //    case Enums.ProtocolType.Socks:
            //        remark = string.Empty;
            //        if (!string.IsNullOrWhiteSpace(data.Remark))
            //        {
            //            remark = "#" + WebUtility.UrlEncode(data.Remark);
            //        }
            //        url = string.Format("{0}:{1}@{2}:{3}",
            //            data.Security,
            //            data.Id,
            //            data.Address,
            //            data.Port);
            //        result = Consts.SocksProtocolPrefix + Convert.ToBase64String(Encoding.UTF8.GetBytes(url)) + remark;
            //        break;
            //    case Enums.ProtocolType.Shadowsocks:
            //        remark = string.Empty;
            //        if (!string.IsNullOrWhiteSpace(data.Remark))
            //        {
            //            remark = "#" + WebUtility.UrlEncode(data.Remark);
            //        }
            //        url = string.Format("{0}:{1}@{2}:{3}",
            //            data.Security,
            //            data.Id,
            //            data.Address,
            //            data.Port);
            //        result = Consts.ShadowSocksProtocolPrefix + Convert.ToBase64String(Encoding.UTF8.GetBytes(url)) + remark;
            //        break;
            //    case Enums.ProtocolType.VMess:
            //        VMessQRCode qrCode = new VMessQRCode()
            //        {
            //            v = data.ConfigVersion.ToString(),
            //            ps = data.Remark?.Trim() ?? string.Empty, //备注也许很长 ;
            //            add = data.Address,
            //            port = data.Port.ToString(),
            //            id = data.Id,
            //            aid = data.AlterId.ToString(),
            //            net = data.Network,
            //            type = data.HeaderType,
            //            host = data.RequestHost,
            //            path = data.Path,
            //            tls = data.StreamSecurity,
            //            sni = data.SNI
            //        };
            //        result = Consts.VMessProtocolPrefix + Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(qrCode)));
            //        break;
            //    case Enums.ProtocolType.VLESS:
            //        remark = string.Empty;
            //        if (!string.IsNullOrWhiteSpace(data.Remark))
            //        {
            //            remark = "#" + WebUtility.UrlEncode(data.Remark);
            //        }
            //        var dicQuery = new Dictionary<string, string>();
            //        if (!string.IsNullOrWhiteSpace(data.Flow))
            //        {
            //            dicQuery.Add("flow", data.Flow);
            //        }
            //        if (!string.IsNullOrWhiteSpace(data.Security))
            //        {
            //            dicQuery.Add("encryption", data.Security);
            //        }
            //        else
            //        {
            //            dicQuery.Add("encryption", "none");
            //        }
            //        if (!string.IsNullOrWhiteSpace(data.StreamSecurity))
            //        {
            //            dicQuery.Add("security", data.StreamSecurity);
            //        }
            //        else
            //        {
            //            dicQuery.Add("security", "none");
            //        }
            //        if (!string.IsNullOrWhiteSpace(data.SNI))
            //        {
            //            dicQuery.Add("sni", data.SNI);
            //        }
            //        if (!string.IsNullOrWhiteSpace(data.Network))
            //        {
            //            dicQuery.Add("type", data.Network);
            //        }
            //        else
            //        {
            //            dicQuery.Add("type", "tcp");
            //        }

            //        switch (data.Network)
            //        {
            //            case "tcp":
            //                if (!string.IsNullOrWhiteSpace(data.HeaderType))
            //                {
            //                    dicQuery.Add("headerType", data.HeaderType);
            //                }
            //                else
            //                {
            //                    dicQuery.Add("headerType", "none");
            //                }
            //                if (!string.IsNullOrWhiteSpace(data.RequestHost))
            //                {
            //                    dicQuery.Add("host", WebUtility.UrlEncode(data.RequestHost));
            //                }
            //                break;
            //            case "kcp":
            //                if (!string.IsNullOrWhiteSpace(data.HeaderType))
            //                {
            //                    dicQuery.Add("headerType", data.HeaderType);
            //                }
            //                else
            //                {
            //                    dicQuery.Add("headerType", "none");
            //                }
            //                if (!string.IsNullOrWhiteSpace(data.Path))
            //                {
            //                    dicQuery.Add("seed", WebUtility.UrlEncode(data.Path));
            //                }
            //                break;

            //            case "ws":
            //                if (!string.IsNullOrWhiteSpace(data.RequestHost))
            //                {
            //                    dicQuery.Add("host", WebUtility.UrlEncode(data.RequestHost));
            //                }
            //                if (!string.IsNullOrWhiteSpace(data.Path))
            //                {
            //                    dicQuery.Add("path", WebUtility.UrlEncode(data.Path));
            //                }
            //                break;

            //            case "http":
            //            case "h2":
            //                dicQuery["type"] = "http";
            //                if (!string.IsNullOrWhiteSpace(data.RequestHost))
            //                {
            //                    dicQuery.Add("host", WebUtility.UrlEncode(data.RequestHost));
            //                }
            //                if (!string.IsNullOrWhiteSpace(data.Path))
            //                {
            //                    dicQuery.Add("path", WebUtility.UrlEncode(data.Path));
            //                }
            //                break;

            //            case "quic":
            //                if (!string.IsNullOrWhiteSpace(data.HeaderType))
            //                {
            //                    dicQuery.Add("headerType", data.HeaderType);
            //                }
            //                else
            //                {
            //                    dicQuery.Add("headerType", "none");
            //                }
            //                dicQuery.Add("quicSecurity", WebUtility.UrlEncode(data.RequestHost));
            //                dicQuery.Add("key", WebUtility.UrlEncode(data.Path));
            //                break;
            //            case "grpc":
            //                if (!string.IsNullOrWhiteSpace(data.Path))
            //                {
            //                    dicQuery.Add("serviceName", WebUtility.UrlEncode(data.Path));
            //                    if (data.HeaderType == Consts.GRPCGunMode || data.HeaderType == Consts.GRPCMultiMode)
            //                    {
            //                        dicQuery.Add("mode", WebUtility.UrlEncode(data.HeaderType));
            //                    }
            //                }
            //                break;
            //        }
            //        query = "?" + string.Join("&", dicQuery.Select(x => x.Key + "=" + x.Value).ToArray());
            //        url = string.Format("{0}@{1}:{2}",
            //        data.Id,
            //        GetIpv6(data.Address),
            //        data.Port);
            //        result = string.Format("{0}{1}{2}{3}", Consts.VLESSProtocolPrefix, url, query, remark);
            //        break;
            //    case Enums.ProtocolType.Trojan:
            //        remark = string.Empty;
            //        if (!string.IsNullOrEmpty(data.Remark))
            //        {
            //            remark = "#" + WebUtility.UrlEncode(data.Remark);
            //        }
            //        query = string.Empty;
            //        if (!string.IsNullOrEmpty(data.SNI))
            //        {
            //            query = string.Format("?sni={0}", WebUtility.UrlEncode(data.SNI));
            //        }
            //        url = string.Format("{0}@{1}:{2}",
            //            data.Id,
            //            GetIpv6(data.Address),
            //            data.Port);
            //        result = string.Format("{0}{1}{2}{3}", Consts.TrojanProtocolPrefix, url, query, remark);
            //        break;
            //    default:
            //        break;
            //}
            return Ok(result);
        }

        private static string GetIpv6(string address)
        {
            return Utils.IsIpv6(address) ? $"[{address}]" : address;
        }
    }
}
