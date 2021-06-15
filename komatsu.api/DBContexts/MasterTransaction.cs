using System;

namespace komatsu.api.DBContexts
{
    public class MasterTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Param { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
