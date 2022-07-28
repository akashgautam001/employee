using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CrudWithStoreprocedure.Models
{
    public class DataAccessLayer
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ToString());
        public string InsertData(Customer objcust)
        {           
            string result = "";
            try
            {
                SqlCommand cmd = new SqlCommand("tbl_CrudDemo_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Age", objcust.Age);
                cmd.Parameters.AddWithValue("@phone", objcust.Phone);
                cmd.Parameters.AddWithValue("@City", objcust.City);
                cmd.Parameters.AddWithValue("@Email", objcust.Email);
                cmd.Parameters.AddWithValue("@Password", objcust.Password);
                con.Open();
                cmd.ExecuteNonQuery();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }
        public string UpdateData(Customer objcust)
        {
            string result = "";
            try
            {

                SqlCommand cmd = new SqlCommand("UpdateCrud", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", objcust.Name);
                cmd.Parameters.AddWithValue("@Age", objcust.Age);
                cmd.Parameters.AddWithValue("@phone", objcust.Phone);
                cmd.Parameters.AddWithValue("@City", objcust.City);
                cmd.Parameters.AddWithValue("@Id", objcust.id);
                con.Open();
                result = cmd.ExecuteScalar().ToString();
                return result;
            }
            catch
            {
                return result = "";
            }
            finally
            {
                con.Close();
            }
        }

        public int DeleteData(int id)
        {
         
            int result;
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                result = cmd.ExecuteNonQuery();
                return result;
                con.Close();
            }
            catch
            {
                return result = 0;
            }
           
        }

        public List<Customer> ShowList()
        {
            List<Customer> Custmarlist = new List<Customer>();
            SqlCommand cmd = new SqlCommand("procSeletelist", con);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sa.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Custmarlist.Add(new Customer
                {
                    id = Convert.ToInt32(dt.Rows[i]["Id"]),
                    Name = dt.Rows[i]["Name"].ToString(),
                    Age = dt.Rows[i]["Age"].ToString(),
                    Phone = dt.Rows[i]["phone"].ToString(),
                    City = dt.Rows[i]["City"].ToString()

                });
            }

            return Custmarlist;
        }
        public Customer SelectDatabyID(int id)
        {
           
            try
            {
                Customer cobj = new Customer();
                SqlCommand cmd = new SqlCommand("GetById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    cobj = new Customer();
                    cobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    cobj.Age = ds.Tables[0].Rows[i]["Age"].ToString();
                    cobj.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();
                    cobj.City = ds.Tables[0].Rows[i]["City"].ToString();
                }
                return cobj;
            }
            //catch
            //{
            //    return cobj;
            //}
            finally
            {
                con.Close();
            }
        }


        public Customer UserLogin(string Email, string Password)
        {

            try
            {
                Customer cobj = new Customer();
                SqlCommand cmd = new SqlCommand("UserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email",Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                return cobj;
            }
            finally
            {
                con.Close();
            }
        }
    }
} 
 