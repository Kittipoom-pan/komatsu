using System;

namespace komatsu.api.DBContexts
{
    public class SystemVersion
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public string Device { get; set; }
        public bool IsForce { get; set; }
        public string StoreLink { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
