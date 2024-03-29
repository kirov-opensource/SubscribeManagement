﻿namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class Connection : BaseEntity
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string ProtocolCode { get; set; }
    }
}
