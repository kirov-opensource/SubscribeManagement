using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class ProtocolParseRule : BaseEntity
    {
        public string Name { get; set; }
        [SQLite.Indexed]
        public string Code { get; set; }
        public string ParseScript { get; set; }
    }
}
