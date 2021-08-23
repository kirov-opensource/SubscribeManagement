namespace SubscribeManagement.WebAPI.DA.Entities
{
    public class SubscribeParseRule
    {
        public string Name { get; set; }
        [SQLite.Indexed]
        public string Code { get; set; }
        //public string SupportedProtocolCode { get; set; }
        public string ParseScript { get; set; }
    }
}
