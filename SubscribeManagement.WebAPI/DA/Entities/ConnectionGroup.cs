using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class ConnectionGroup : BaseEntity
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}
