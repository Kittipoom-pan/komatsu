using System;

namespace komatsu.api.Model.Response
{
    public class Response
    {
        public Object Message { get; set; }
        public Response(Object data)
        {
            this.Message = data;
        }
    }
}
