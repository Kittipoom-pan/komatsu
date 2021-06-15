namespace komatsu.api.Model.Request
{
    public class CalculateTCORequest
    {
        public string CustomerName { get; set; }
        public string MoDelID1 { get; set; }
        public string MoDelID2 { get; set; }
        public string Country { get; set; }
        public int LifeTime { get; set; }
        public string WorkingMode { get; set; }
        public int LingRatioID { get; set; }
        public float ModelPrice1 { get; set; }
        public float ModelPrice2 { get; set; }
        public float ModelBucket1 { get; set; }
        public float ModelBucket2 { get; set; }
    }
}
