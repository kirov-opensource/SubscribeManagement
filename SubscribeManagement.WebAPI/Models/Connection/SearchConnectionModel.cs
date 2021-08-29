using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Models.Connection
{
    public class SearchConnectionModel
    {
        public DateTime? CreateAtBegin { get; set; }
        public DateTime? CreateAtEnd { get; set; }
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
