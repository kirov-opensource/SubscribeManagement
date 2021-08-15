using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class ConnectionExtraProperty : BaseEntity
    {
        [SQLite.Indexed]
        public long ConnectionId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
