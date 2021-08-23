using SQLite;
using System;

namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class BaseEntity
    {
        [PrimaryKey]
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
