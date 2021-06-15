using System.Data;

namespace komatsu.api.Model.Response
{
    public class CalculateTCOResponse
    {
        public string WorkingMode1 { get; set; }
        public string ModelName1 { get; set; }
        public string WorkingMode2 { get; set; }
        public string ModelName2 { get; set; }
        public string TcoRadio1 { get; set; }
        public string TcoRadio2 { get; set; }
        public string TcoProd1 { get; set; }
        public string ProdRadio1 { get; set; }
        public string ProdRadio2 { get; set; }
        public string TcoProd2 { get; set; }
        public string ETcoRadio1 { get; set; }
        public string EProdRadio1 { get; set; }
        public string ETcoProd1 { get; set; }
        public string ETcoRadio2 { get; set; }
        public string EProdRadio2 { get; set; }
        public string ETcoProd2 { get; set; }
        public string FuelConsumRadio1 { get; set; }
        public string FuelConsumRadio2 { get; set; }
        public string Initial1 { get; set; }
        public string Fuel1 { get; set; }
        public string Mainten1 { get; set; }
        public string Labor1 { get; set; }
        public string Resale1 { get; set; }
        public string TCO1 { get; set; }
        public string Initial2 { get; set; }
        public string Fuel2 { get; set; }
        public string Mainten2 { get; set; }
        public string Labor2 { get; set; }
        public string Resale2 { get; set; }
        public string TCO2 { get; set; }

        public void getDataResultEconomy(DataRow dr)
        {
            TcoRadio1 = dr["rTcoRadio1"].ToString();
            TcoRadio2 = dr["rTcoRadio2"].ToString();
            TcoProd1 = dr["rTcoProd1"].ToString();
            TcoProd2 = dr["rTcoProd2"].ToString();
            ProdRadio1 = dr["rProdRadio1"].ToString();
            ProdRadio2 = dr["rProdRadio2"].ToString();
            ETcoProd1 = dr["rETcoProd1"].ToString();
            ETcoRadio1 = dr["rETcoRadio1"].ToString();
            EProdRadio1 = dr["rEProdRadio1"].ToString();
            EProdRadio2 = dr["rEProdRadio2"].ToString();
            ETcoRadio2 = dr["rETcoRadio2"].ToString();
            ETcoProd2 = dr["rETcoProd2"].ToString();
            Initial1 = dr["rInitial1"].ToString();
            Initial2 = dr["rInitial2"].ToString();
            Fuel1 = dr["rFuel1"].ToString();
            Fuel2 = dr["rFuel2"].ToString();
            Mainten1 = dr["rMainten1"].ToString();
            Mainten2 = dr["rMainten2"].ToString();
            Labor1 = dr["rLabor1"].ToString();
            Labor2 = dr["rLabor2"].ToString();
            Resale1 = dr["rResale1"].ToString();
            Resale2 = dr["rResale2"].ToString();
            TCO1 = dr["rTCO1"].ToString();
            TCO2 = dr["rTCO2"].ToString();
            FuelConsumRadio1 = dr["rFuelConsumRadio1"].ToString();
            FuelConsumRadio2 = dr["rFuelConsumRadio2"].ToString();
            WorkingMode1 = dr["pWorkingMode1"].ToString();
            ModelName1 = dr["pModelName1"].ToString();
            WorkingMode2 = dr["pWorkingMode2"].ToString();
            ModelName2 = dr["pModelName2"].ToString();
        }
    }
}
