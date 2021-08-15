using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class ConnectionGroupItem : BaseEntity
    {
        [SQLite.Indexed]
        public long GroupId { get; set; }
        public long ConnectionId { get; set; }
    }
}
