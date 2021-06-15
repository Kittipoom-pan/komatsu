using komatsu.api.Model.Request;
using komatsu.api.Model.Response;

namespace komatsu.api.Model
{
    public class Calculate3YC
    {
        public Calculate3YCRequest Calculate3YCRequest { get; set; }
        public Calculate3YCResponse Calculate3YCResponse { get; set; }
        public Calculate3YC(Calculate3YCRequest calculate3YCRequest, Calculate3YCResponse calculate3YCResponse)
        {
            this.Calculate3YCRequest = calculate3YCRequest;
            this.Calculate3YCResponse = calculate3YCResponse;
        }
        //public class TCORequest
        //{
        //    public object request { get; set; }

        //    public TCORequest(object request)
        //    {
        //        this.request = request;
        //    }
        //}

        //public class TCOResponse
        //{
        //    public object response { get; set; }
        //    public TCOResponse(object response)
        //    {
        //        this.response = response;
        //    }
        //}
    }
}
