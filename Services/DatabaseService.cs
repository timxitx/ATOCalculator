using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ATOCalculator.Services
{
    public class DatabaseService
    {
        public int InsertData(string query)
        {
            string cs = "data source = DESKTOP-I7272U8; database = test; Integrated security = true";
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            //cmd.ExecuteNonQuery();
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return id;
        }
    }
}
