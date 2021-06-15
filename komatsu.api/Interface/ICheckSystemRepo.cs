using komatsu.api.Model.Response;
using System.Threading.Tasks;

namespace komatsu.api.Interface
{
    public interface ICheckSystemRepo
    {
        Task<CheckSystemResponse> CheckSystem(string version, string device);
    }
}
