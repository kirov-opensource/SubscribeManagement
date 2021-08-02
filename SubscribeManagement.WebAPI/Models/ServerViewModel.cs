using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Models
{
    public class ServerViewModel
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string ConfigVersion { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 额外Id
        /// </summary>
        public string AlterId { get; set; }
        /// <summary>
        /// 安全策略
        /// </summary>
        public string Security { get; set; }
        /// <summary>
        /// 网络 tcp,kcp,ws,h2,quic
        /// </summary>
        public string Network { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 伪装类型
        /// </summary>
        public string HeaderType
        {
            get; set;
        }
        /// <summary>
        /// 伪装的域名
        /// </summary>
        public string RequestHost
        {
            get; set;
        }

        /// <summary>
        /// ws h2 path
        /// </summary>
        public string Path
        {
            get; set;
        }

        /// <summary>
        /// 底层传输安全
        /// </summary>
        public string StreamSecurity
        {
            get; set;
        }

        /// <summary>
        /// 是否允许不安全连接（用于客户端）
        /// </summary>
        public string AllowInsecure
        {
            get; set;
        }


        /// <summary>
        /// config type(1=normal,2=custom)
        /// </summary>
        public int ConfigType
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TestResult
        {
            get; set;
        }

        /// <summary>
        /// SubItem id
        /// </summary>
        public string SubId
        {
            get; set;
        }

        /// <summary>
        /// VLESS flow
        /// </summary>
        public string Flow
        {
            get; set;
        }
    }
}
