namespace komatsu.api.Model.Response
{
    public class CheckSystemResponse
    {
        public bool IsForce { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StoreLink { get; set; }
        public string Version { get; set; }
    }
}
