using komatsu.api.DBContexts;
using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace komatsu.api.Interface
{
    public interface IResultRepo
    {
        Task<CalculateTCOResponse> GetCalculateTCO(CalculateTCORequest calculateTCO, string token);
        Task<Calculate3YCResponse> GetCalculate3YC(Calculate3YCRequest calculate3YC, string token);
        Task<ResponseKms<List<MasterTransaction>>> GetHistory(string token, string type, string app_version, string device_type);
        Task<ResponseKms<SystemVersion>> CheckSystem(string app_version, string device_type);
    }
}
