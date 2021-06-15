namespace komatsu.api.Model.Request
{
    public class Calculate3YCRequest
    {
        public string CustomerName { get; set; }
        public string MoDelName { get; set; }
        public float PriceVat { get; set; }
        public string Country { get; set; }
        public float ResaleValue { get; set; }
        public float LeasingTerm { get; set; }
        public float InterestRate { get; set; }
        public float DownPayment { get; set; }
        public float LifetimeWork { get; set; }
        public float FuelConsumption { get; set; }
        public float FuelPrice { get; set; }
        public float MaintenCost { get; set; }
        public float RepairCost { get; set; }
    }
}
