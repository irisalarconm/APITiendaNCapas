using Microsoft.Extensions.Configuration;
using ProjectCrud.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCrud.DAL.Data.Repository
{
    public class DataProduct : IGenericRepository<Product>
    {
        private readonly IConfiguration _configuration;

        public DataProduct(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            string connection = _configuration.GetConnectionString("TiendaGuau");
            return new SqlConnection(connection);
        }


        //!GET ALL PRODUCTS
        public List<Product> GetAll()
        {
            List<Product> productsList = new List<Product>();

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_GetProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                connection.Open();
                adapter.Fill(dt);
                connection.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    productsList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ClientId = Convert.ToInt32(dr["ClientId"]),
                        NameProduct = dr["NameProduct"].ToString(),
                        Description = dr["Description"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"])

                    });
                }
            }

            return productsList;
        }

        //!INSERT PRODUCT

        public bool Create(Product product)
        {
            int x = 0;

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClientId", product.ClientId);
                cmd.Parameters.AddWithValue("@NameProduct", product.NameProduct);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);

                connection.Open();
                x = cmd.ExecuteNonQuery();
                connection.Close();
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

        //!GET PRODUCT BY ID

        public Product GetById(int id)
        {
            Product product = null;

            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetProductById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new Product
                            {
                                ProductId = Convert.ToInt32(reader["@ProductId"]),
                                ClientId = Convert.ToInt32(reader["@ClientId"]),
                                NameProduct = reader["@NameProduct"].ToString(),
                                Description = reader["@Description"].ToString(),
                                Price = Convert.ToDecimal(reader["@Price"])
                            };
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en GetClientById: " + ex.Message);
            }

            return product;

        }


        //!UPDATE PRODUCT

        public bool Update(Product product)
        {
            int x = 0;
            using (SqlConnection connection = GetConnection())
            {

                SqlCommand cmd = new SqlCommand("sp_UpdateProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ClientId", product.ClientId);
                cmd.Parameters.AddWithValue("@NameProduct", product.NameProduct);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);


                connection.Open();
                x = cmd.ExecuteNonQuery();
                connection.Close();

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


        //!DELETE PRODUCT

        public string Delete(int id)
        {
            string result = "";

            using (SqlConnection connection = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteProduct", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                cmd.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["@OUTPUTMESSAGE"].Value.ToString();
                connection.Close();
            }

            return result;

        }
    }
}
