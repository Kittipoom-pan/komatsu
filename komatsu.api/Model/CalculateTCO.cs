using komatsu.api.Model.Request;
using komatsu.api.Model.Response;

namespace komatsu.api.Model
{
    public class CalculateTCO
    {
        public CalculateTCORequest calculateTCORequest { get; set; }
        public CalculateTCOResponse calculateTCOResponse { get; set; }
        public CalculateTCO(CalculateTCORequest calculateTCORequest, CalculateTCOResponse calculateTCOResponse)
        {
            this.calculateTCORequest = calculateTCORequest;
            this.calculateTCOResponse = calculateTCOResponse;
        }
    }
}
