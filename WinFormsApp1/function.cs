using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp1
{
    internal class function
    {
        protected SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\DUC DAI\\Desktop\\LapTrinhWindows\\BTL\\Database\\dbHotel.mdf;Integrated Security=True;Connect Timeout=30";
            return conn;
        }

        public bool validateLogin (string username, string password)
        {
            string query = "SELECT COUNT(*) FROM usersList WHERE username = @username AND password = @password";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlParameter usernameParam = new SqlParameter("@username", SqlDbType.VarChar, 16);
            usernameParam.Value = username;
            cmd.Parameters.Add(usernameParam);

            SqlParameter passwordParam = new SqlParameter("@password", SqlDbType.VarChar, 32);
            passwordParam.Value = password;
            cmd.Parameters.Add(passwordParam);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();

            return count > 0;
        }

        public string getUserRole(string username)
        {
            string query = "SELECT role FROM usersList WHERE username = @username";
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlParameter usernameParam = new SqlParameter("username", SqlDbType.VarChar, 16);
            usernameParam.Value = username;
            cmd.Parameters.Add(usernameParam);

            conn.Open();
            string role = (string)cmd.ExecuteScalar();
            conn.Close();
            return role;
        }

        public int getNumberData(string query)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand(query, conn);

            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count;
        }

        public DataSet getData(String query)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void setData(String query, String message)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public SqlDataReader getForCombo(String query)
        {
            SqlConnection conn = GetConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd = new SqlCommand(query, conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            return sdr;
        }
    }
}