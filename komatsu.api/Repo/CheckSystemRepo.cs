using komatsu.api.DBContexts;
using komatsu.api.Interface;
using komatsu.api.Model.Response;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace komatsu.api.Repo
{
    public class CheckSystemRepo : ICheckSystemRepo
    {
        private readonly devkomatsuContext _context;
        public CheckSystemRepo(devkomatsuContext context)
        {
            _context = context;
        }

        public async Task<CheckSystemResponse> CheckSystem(string version, string device)
        {
            MySqlConnection con = new MySqlConnection(_context.Database.GetDbConnection().ConnectionString);
            try
            {
                SystemVersion system = null;
                DataTable table = new DataTable();

                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    con.Open();
                    cmd.CommandText = "check_system";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@pDevice", device);
                    cmd.Parameters.AddWithValue("@pVersion", version);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            table.Load(reader);
                        }
                    }

                    if (table != null && table.Rows.Count > 0)
                    {
                        DataRow dr = table.Rows[0];
                        system = new SystemVersion();

                        system.Device = dr["device"].ToString();
                        system.StoreLink = dr["store_link"].ToString();
                        system.IsForce = Convert.ToBoolean(dr["is_force"].ToString());
                        system.Version = dr["version"].ToString();
                    }
                }

                if (system != null)
                {
                    if (system.IsForce)
                    {
                        return new CheckSystemResponse
                        {
                            Success = false,
                            IsForce = true,
                            Message = "App have new version. You must force update app before.",
                            StoreLink = system.StoreLink,
                            Version = system.Version,
                        };
                    }
                    else
                    {
                        return new CheckSystemResponse
                        {
                            Success = false,
                            IsForce = false,
                            Message = "App have new version. You must soft update app before.",
                            StoreLink = system.StoreLink,
                            Version = system.Version,
                        };
                    }
                }

                return new CheckSystemResponse
                {
                    Success = true,
                    IsForce = false
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
