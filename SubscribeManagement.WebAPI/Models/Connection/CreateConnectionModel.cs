using AutoMapper;
using System.Collections.Generic;

namespace SubscribeManagement.WebAPI.Models.Connection
{
    public class CreateConnectionModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 协议
        /// </summary>
        public string ProtocolCode { get; set; }
        /// <summary>
        /// 额外属性
        /// </summary>
        [IgnoreMap]
        public Dictionary<string, string> ExtraProperties { get; set; }
    }
}
