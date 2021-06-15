namespace komatsu.api.Model.Response
{
    public class ResponseKms<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public CheckSystemResponse CheckSystem { get; set; }
        public T Data { get; set; }
    }
}
