using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectCrud.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.DAL.Data.Repository
{
    public class DataClient : IGenericRepository<Client>
    {
        private readonly IConfiguration _configuration;
        ILogger<DataClient> _logger;

        public DataClient(IConfiguration configuration, ILogger<DataClient> logger)
        {
            _configuration = configuration;
            _logger = logger;

        }

        public SqlConnection GetConnection()
        {
            string connection = _configuration.GetConnectionString("TiendaGuau");
            return new SqlConnection(connection);
        }

        //!GET CLIENTS

        public List<Client> GetAll()
        {
            try
            {
                List<Client> clientList = new List<Client>();

                using (GetConnection())
                {
                    SqlCommand cmd = GetConnection().CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetClients";
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtClients = new DataTable();

                    GetConnection().Open();
                    adapter.Fill(dtClients);
                    GetConnection().Close();

                    foreach (DataRow dr in dtClients.Rows)
                    {
                        clientList.Add(new Client
                        {
                            ClientId = Convert.ToInt32(dr["ClientId"]),
                            NameClient = dr["NameClient"].ToString(),
                            LastnameClient = dr["LastnameClient"].ToString(),
                            DNIClient = Convert.ToInt64(dr["DNIClient"]),
                            AdressClient = dr["AdressClient"].ToString(),
                            Phone = Convert.ToInt64(dr["Phone"]),
                            status = (Status)Convert.ToInt16(dr["status"])
                        });
                    }
                }

                return clientList;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            

        }

        //!INSERT CLIENTS


        public bool Create(Client client)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_InsertClients", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NameClient", client.NameClient);
                    cmd.Parameters.AddWithValue("@LastnameClient", client.LastnameClient);
                    cmd.Parameters.AddWithValue("@DNIClient", client.DNIClient);
                    cmd.Parameters.AddWithValue("@AdressClient", client.AdressClient);
                    cmd.Parameters.AddWithValue("@Phone", client.Phone);
                    cmd.Parameters.AddWithValue("@status", client.status);

                    connection.Open();
                    x = cmd.ExecuteNonQuery();
                    connection.Close();

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            if (x > 0)
            {
                return true;
            }
            else
            {
                _logger.LogError("Algo paso");
                return false;
            }

        }


        //! GET PRODUCT BY ID
        public Client GetById(int id)
        {
            Client client = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetClientById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = "sp_GetClientById";
                    cmd.Parameters.AddWithValue("@ClientId", id);
                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);



                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                ClientId = Convert.ToInt32(reader["ClientId"]),
                                NameClient = reader["NameClient"].ToString(),
                                LastnameClient = reader["LastnameClient"].ToString(),
                                DNIClient = Convert.ToInt64(reader["DNIClient"]),
                                AdressClient = reader["AdressClient"].ToString(),
                                Phone = Convert.ToInt64(reader["Phone"]),
                                status = (Status)Convert.ToInt16(reader["status"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en GetClientById: " + ex.Message);
            }

            return client;
        }


        //!UPDATE CLIENT
        public bool Update(Client client)
        {
            int x = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_UpdateClients", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", client.ClientId);
                    cmd.Parameters.AddWithValue("@NameClient", client.NameClient);
                    cmd.Parameters.AddWithValue("@LastnameClient", client.LastnameClient);
                    cmd.Parameters.AddWithValue("@DNIClient", client.DNIClient);
                    cmd.Parameters.AddWithValue("@AdressClient", client.AdressClient);
                    cmd.Parameters.AddWithValue("@Phone", client.Phone);
                    cmd.Parameters.AddWithValue("@status", client.status);


                    //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //DataTable dtClients = new DataTable();

                    connection.Open();
                    x = cmd.ExecuteNonQuery();
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en Update: " + ex.Message);
            }


            if (x > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //!DELETE CLIENT
        public string Delete(int id)
        {
            string result = "";
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteClient", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClientId", id);
                    cmd.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    result = cmd.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error in delete: " + ex.Message);
            }

            return result;

        }

        public bool DNIUnique(long DNI)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {

                    SqlCommand cmd = new SqlCommand("sp_GetDNI", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DNIClient", DNI);

                    connection.Open();
                    int result = (int)cmd.ExecuteScalar(); //metodo para recuperar un dato o valor unico
                    connection.Close();

                    return result == 1;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error en Unique: " + ex.Message);
                return false;
            }
            
        }

    }
}

