namespace komatsu.api.Model.Response
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public CheckSystemResponse CheckSystem { get; set; }

    }
}
