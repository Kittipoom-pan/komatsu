using komatsu.api.DBContexts;
using komatsu.api.Interface;
using komatsu.api.Model;
using komatsu.api.Model.Request;
using komatsu.api.Model.Response;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace komatsu.api.Repo
{
    public class ResultRepo : IResultRepo
    {
        private readonly devkomatsuContext _context;
        private readonly MySqlConnection _con;
        private readonly ICheckSystemRepo _checkSystem;
        public ResultRepo(devkomatsuContext context, ICheckSystemRepo checkSystem)
        {
            _checkSystem = checkSystem;
            _context = context;
            _con = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString);
        }

        public async Task<Calculate3YCResponse> GetCalculate3YC(Calculate3YCRequest calculate3YC, string token)
        {
            Calculate3YCResponse result = null;
            DataTable table = new DataTable();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.CommandText = "get3YCResult";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pCustomerName", calculate3YC.CustomerName);
                    cmd.Parameters.AddWithValue("@pCountry", calculate3YC.Country);
                    cmd.Parameters.AddWithValue("@pModelName", calculate3YC.MoDelName);
                    cmd.Parameters.AddWithValue("@pPriceVat", calculate3YC.PriceVat);
                    cmd.Parameters.AddWithValue("@pResaleValue", calculate3YC.ResaleValue);
                    cmd.Parameters.AddWithValue("@pLeasingTerm", calculate3YC.LeasingTerm);
                    cmd.Parameters.AddWithValue("@pInterestRate", calculate3YC.InterestRate);
                    cmd.Parameters.AddWithValue("@pDownPayment", calculate3YC.DownPayment);
                    cmd.Parameters.AddWithValue("@pLifetimeWork", calculate3YC.LifetimeWork);
                    cmd.Parameters.AddWithValue("@pFuelConsumption", calculate3YC.FuelConsumption);
                    cmd.Parameters.AddWithValue("@pFuelPrice", calculate3YC.FuelPrice);
                    cmd.Parameters.AddWithValue("@pMaintenCost", calculate3YC.MaintenCost);
                    cmd.Parameters.AddWithValue("@pRepairCost", calculate3YC.RepairCost);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        result = new Calculate3YCResponse();

                        foreach (DataRow row in table.Rows)
                        {
                            result.getData(row);
                        }
                    }
                }
                catch (System.IO.IOException e)
                {
                    throw e;
                }

                finally
                {
                    _con.Close();
                }
            }

            await AddTransaction3YC(calculate3YC, result, token);

            return result;
        }

        public async Task<CalculateTCOResponse> GetCalculateTCO(CalculateTCORequest calculateTCO, string token)
        {
            CalculateTCOResponse result = null;
            DataTable table = new DataTable();

            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.CommandText = "getResult";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pCustomerName", calculateTCO.CustomerName);
                    cmd.Parameters.AddWithValue("@pModelID1", calculateTCO.MoDelID1);
                    cmd.Parameters.AddWithValue("@pModelID2", calculateTCO.MoDelID2);
                    cmd.Parameters.AddWithValue("@pCountry", calculateTCO.Country);
                    cmd.Parameters.AddWithValue("@pLifetime", calculateTCO.LifeTime);
                    cmd.Parameters.AddWithValue("@pWorkingMode", calculateTCO.WorkingMode);
                    cmd.Parameters.AddWithValue("@pIdlingRatio", calculateTCO.LingRatioID);
                    cmd.Parameters.AddWithValue("@pModelPrice1", calculateTCO.ModelPrice1);
                    cmd.Parameters.AddWithValue("@pModelPrice2", calculateTCO.ModelPrice2);
                    cmd.Parameters.AddWithValue("@pModelBucket1", calculateTCO.ModelBucket1);
                    cmd.Parameters.AddWithValue("@pModelBucket2", calculateTCO.ModelBucket2);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        result = new CalculateTCOResponse();

                        foreach (DataRow row in table.Rows)
                        {
                            result.getDataResultEconomy(row);
                        }
                    }
                }
                catch (System.IO.IOException e)
                {
                    throw e;
                }

                finally
                {
                    _con.Close();
                }
            }

            await AddTransactionTCO(calculateTCO, result, token);

            return result;
        }

        public async Task<List<MasterModel>> GetModel(string app_version, string device_type)
        {
            var result = _checkSystem.CheckSystem(app_version, device_type);

            return _context.MasterModel.ToList();
        }

        public async Task<User> GetUser(string token)
        {
            var jwtToken = token.Substring(7);

            var user = await _context.User.Where(e => e.Token == jwtToken).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> AddTransaction3YC(Calculate3YCRequest calculate3YC, Calculate3YCResponse calculate3YCResponse, string token)
        {
            bool success = false;
            DataTable table = new DataTable();

            Calculate3YC calculateTCO = new Calculate3YC(calculate3YC, calculate3YCResponse);

            string json = JsonConvert.SerializeObject(calculateTCO);

            var user = await GetUser(token);

            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.CommandText = "addTransaction3YC";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (user != null)
                    {
                        cmd.Parameters.AddWithValue("@pUserID", user.Id);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pUserID", 0);
                    }
                    cmd.Parameters.AddWithValue("@pCustomerName", calculate3YC.CustomerName);
                    cmd.Parameters.AddWithValue("@pCountry", calculate3YC.Country);
                    cmd.Parameters.AddWithValue("@pModelName", calculate3YC.MoDelName);
                    cmd.Parameters.AddWithValue("@pPriceVat", calculate3YC.PriceVat);
                    cmd.Parameters.AddWithValue("@pResaleValue", calculate3YC.ResaleValue);
                    cmd.Parameters.AddWithValue("@pLeasingTerm", calculate3YC.LeasingTerm);
                    cmd.Parameters.AddWithValue("@pInterestRate", calculate3YC.InterestRate);
                    cmd.Parameters.AddWithValue("@pDownPayment", calculate3YC.DownPayment);
                    cmd.Parameters.AddWithValue("@pLifetimeWork", calculate3YC.LifetimeWork);
                    cmd.Parameters.AddWithValue("@pFuelConsumption", calculate3YC.FuelConsumption);
                    cmd.Parameters.AddWithValue("@pFuelPrice", calculate3YC.FuelPrice);
                    cmd.Parameters.AddWithValue("@pMaintenCost", calculate3YC.MaintenCost);
                    cmd.Parameters.AddWithValue("@pRepairCost", calculate3YC.RepairCost);
                    cmd.Parameters.AddWithValue("@mCountry", calculate3YCResponse.Country);
                    cmd.Parameters.AddWithValue("@mVat", calculate3YCResponse.Vat);
                    cmd.Parameters.AddWithValue("@mPriceIDR", calculate3YCResponse.Price);
                    cmd.Parameters.AddWithValue("@mTotalPrice", calculate3YCResponse.TotalPrice);
                    cmd.Parameters.AddWithValue("@mMrv", calculate3YCResponse.Mrv);
                    cmd.Parameters.AddWithValue("@mMrvVat", calculate3YCResponse.MrvVat);
                    cmd.Parameters.AddWithValue("@mAF", calculate3YCResponse.AF);
                    cmd.Parameters.AddWithValue("@mDownPayment", calculate3YCResponse.DownPayment);
                    cmd.Parameters.AddWithValue("@mFinanceAmount", calculate3YCResponse.FinanceAmount);
                    cmd.Parameters.AddWithValue("@mInstallment", calculate3YCResponse.Installment);
                    cmd.Parameters.AddWithValue("@mTotalPayment", calculate3YCResponse.TotalPayment);
                    cmd.Parameters.AddWithValue("@mTotalFuelCost", calculate3YCResponse.TotalFuelCost);
                    cmd.Parameters.AddWithValue("@mTotalMaintenCost", calculate3YCResponse.TotalMaintenCost);
                    cmd.Parameters.AddWithValue("@mTotalRepairCost", calculate3YCResponse.TotalRepairCost);
                    cmd.Parameters.AddWithValue("@mResaleValue", calculate3YCResponse.ResaleValue);
                    cmd.Parameters.AddWithValue("@r3YC", calculate3YCResponse.R3YC);
                    cmd.Parameters.AddWithValue("@r3YCResaleCost", calculate3YCResponse.R3YCResaleCost);
                    cmd.Parameters.AddWithValue("@param", json);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        success = true;
                    }
                }
                catch (System.IO.IOException e)
                {
                    throw e;
                }

                finally
                {
                    _con.Close();
                }

                return success;
            }
        }

        public async Task<bool> AddTransactionTCO(CalculateTCORequest calculateTCO, CalculateTCOResponse calculateTCOResponse, string token)
        {
            bool success = false;
            DataTable table = new DataTable();
            var user = await GetUser(token);

            CalculateTCO calculate = new CalculateTCO(calculateTCO, calculateTCOResponse);
            string json = JsonConvert.SerializeObject(calculate);

            using (MySqlCommand cmd = new MySqlCommand())
            {
                try
                {
                    cmd.Connection = _con;
                    _con.Open();
                    cmd.CommandText = "addTransactionTCO";
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (user != null)
                    {
                        cmd.Parameters.AddWithValue("@pUserID", user.Id);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@pUserID", 0);
                    }
                    cmd.Parameters.AddWithValue("@pCustomerName", calculateTCO.CustomerName);
                    cmd.Parameters.AddWithValue("@pModelID1", calculateTCO.MoDelID1);
                    cmd.Parameters.AddWithValue("@pModelID2", calculateTCO.MoDelID2);
                    cmd.Parameters.AddWithValue("@pCountry", calculateTCO.Country);
                    cmd.Parameters.AddWithValue("@pLifetime", calculateTCO.LifeTime);
                    cmd.Parameters.AddWithValue("@pWorkingMode", calculateTCO.WorkingMode);
                    cmd.Parameters.AddWithValue("@pIdlingRatio", calculateTCO.LingRatioID);
                    cmd.Parameters.AddWithValue("@pModelPrice1", calculateTCO.ModelPrice1);
                    cmd.Parameters.AddWithValue("@pModelPrice2", calculateTCO.ModelPrice2);
                    cmd.Parameters.AddWithValue("@pModelBucket1", calculateTCO.ModelBucket1);
                    cmd.Parameters.AddWithValue("@pModelBucket2", calculateTCO.ModelBucket2);
                    cmd.Parameters.AddWithValue("@pWorkingMode1", calculateTCOResponse.WorkingMode1);
                    cmd.Parameters.AddWithValue("@pWorkingMode2", calculateTCOResponse.WorkingMode2);
                    cmd.Parameters.AddWithValue("@pModelName1", calculateTCOResponse.ModelName1);
                    cmd.Parameters.AddWithValue("@pModelName2", calculateTCOResponse.ModelName2);
                    cmd.Parameters.AddWithValue("@rTcoRadio1", calculateTCOResponse.TcoRadio1);
                    cmd.Parameters.AddWithValue("@rProdRadio1", calculateTCOResponse.ProdRadio1);
                    cmd.Parameters.AddWithValue("@rTcoProd1", calculateTCOResponse.TcoProd1);
                    cmd.Parameters.AddWithValue("@rETcoRadio1", calculateTCOResponse.ETcoRadio1);
                    cmd.Parameters.AddWithValue("@rEProdRadio1", calculateTCOResponse.EProdRadio1);
                    cmd.Parameters.AddWithValue("@rETcoProd1", calculateTCOResponse.ETcoProd1);
                    cmd.Parameters.AddWithValue("@rInitial1", calculateTCOResponse.Initial1);
                    cmd.Parameters.AddWithValue("@rFuel1", calculateTCOResponse.Fuel1);
                    cmd.Parameters.AddWithValue("@rMainten1", calculateTCOResponse.Mainten1);
                    cmd.Parameters.AddWithValue("@rLabor1", calculateTCOResponse.Labor1);
                    cmd.Parameters.AddWithValue("@rResale1", calculateTCOResponse.Resale1);
                    cmd.Parameters.AddWithValue("@rTCO1", calculateTCOResponse.TCO1);
                    cmd.Parameters.AddWithValue("@rFuelConsumRadio1", calculateTCOResponse.FuelConsumRadio1);
                    cmd.Parameters.AddWithValue("@rTcoRadio2", calculateTCOResponse.TcoRadio2);
                    cmd.Parameters.AddWithValue("@rProdRadio2", calculateTCOResponse.ProdRadio2);
                    cmd.Parameters.AddWithValue("@rTcoProd2", calculateTCOResponse.TcoProd2);
                    cmd.Parameters.AddWithValue("@rETcoRadio2", calculateTCOResponse.ETcoRadio2);
                    cmd.Parameters.AddWithValue("@rEProdRadio2", calculateTCOResponse.EProdRadio2);
                    cmd.Parameters.AddWithValue("@rETcoProd2", calculateTCOResponse.ETcoProd2);
                    cmd.Parameters.AddWithValue("@rFuelConsumRadio2", calculateTCOResponse.FuelConsumRadio2);
                    cmd.Parameters.AddWithValue("@rInitial2", calculateTCOResponse.Initial2);
                    cmd.Parameters.AddWithValue("@rFuel2", calculateTCOResponse.Fuel2);
                    cmd.Parameters.AddWithValue("@rMainten2", calculateTCOResponse.Mainten2);
                    cmd.Parameters.AddWithValue("@rLabor2", calculateTCOResponse.Labor2);
                    cmd.Parameters.AddWithValue("@rResale2", calculateTCOResponse.Resale2);
                    cmd.Parameters.AddWithValue("@rTCO2", calculateTCOResponse.TCO2);
                    cmd.Parameters.AddWithValue("@param", json);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        success = true;
                    }
                }
                catch (System.IO.IOException e)
                {
                    throw e;
                }

                finally
                {
                    _con.Close();
                }
            }

            return success;
        }

        public async Task<ResponseKms<List<MasterTransaction>>> GetHistory(string token, string type,string app_version, string device_type)
        {
            ResponseKms<List<MasterTransaction>> obj = new ResponseKms<List<MasterTransaction>>();
            obj.Data = new List<MasterTransaction>();

            var response = await _checkSystem.CheckSystem(app_version, device_type);
            var user = await GetUser(token);

            if (!response.Success)
            {
                obj.CheckSystem = response;

                return obj;
            }

            if (user == null)
            {
                return null;
            }

            var result = _context.MasterTransaction.Where(e => e.Type == type && e.UserId == user.Id).OrderByDescending(e => e.CreatedAt).ToList();

            obj.Status = true;
            obj.Data = result;

            return obj;
        }

        public async Task<ResponseKms<SystemVersion>> CheckSystem(string app_version, string device_type)
        {
            ResponseKms<SystemVersion> obj = new ResponseKms<SystemVersion>();

            var response = await _checkSystem.CheckSystem(app_version,device_type);

            if (!response.Success)
            {
                obj.CheckSystem = response;
            }

            return obj;
        }
    }
}
