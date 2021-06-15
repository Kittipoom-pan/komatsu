using System.Data;

namespace komatsu.api.Model.Response
{
    public class Calculate3YCResponse
    {
        public string Country { get; set; }
        public string Vat { get; set; }
        public string Price { get; set; }
        public string TotalPrice { get; set; }
        public string Mrv { get; set; }
        public string MrvVat { get; set; }
        public string AF { get; set; }
        public string DownPayment { get; set; }
        public string FinanceAmount { get; set; }
        public string Installment { get; set; }
        public string TotalPayment { get; set; }
        public string TotalFuelCost { get; set; }
        public string TotalMaintenCost { get; set; }
        public string TotalRepairCost { get; set; }
        public string ResaleValue { get; set; }
        public string R3YC { get; set; }
        public string R3YCResaleCost { get; set; }
        public void getData(DataRow dr)
        {
            Country = dr["mCountry"].ToString();
            Vat = dr["mVat"].ToString();
            Price = dr["mPrice"].ToString();
            TotalPrice = dr["mTotalPrice"].ToString();
            Mrv = dr["mMrv"].ToString();
            MrvVat = dr["mMrvVat"].ToString();
            AF = dr["mAF"].ToString();
            DownPayment = dr["mDownPayment"].ToString();
            FinanceAmount = dr["mFinanceAmount"].ToString();
            Installment = dr["mInstallment"].ToString();
            TotalPayment = dr["mTotalPayment"].ToString();
            TotalFuelCost = dr["mTotalFuelCost"].ToString();
            TotalMaintenCost = dr["mTotalMaintenCost"].ToString();
            TotalRepairCost = dr["mTotalRepairCost"].ToString();
            ResaleValue = dr["mResaleValue"].ToString();
            R3YC = dr["r3YC"].ToString();
            R3YCResaleCost = dr["r3YCResaleCost"].ToString();
        }
    }
}
