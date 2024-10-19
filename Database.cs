/*using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public class Database
    {
        private readonly string connectionString = "Data Source=.;Initial Catalog=QLSV;User ID=sa;Password=1234;TrustServerCertificate=True;";

        public DataTable SelectData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM tblSinhVien"; // Ensure 'tblSinhVien' is the correct table name
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

            return dt;
        }
    }
}
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public class Database
    {
        private string connectionString = "Data Source=.;Initial Catalog=QLSV;User ID=sa;Password=1234;TrustServerCertificate=True;";
        private SqlConnection conn;
        private DataTable dt;
        private SqlCommand cmd;
        public Database()
        {
            try
            {
                conn = new SqlConnection(connectionString);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable SelectData(string sql,List<CustomParameter> lstPara)
        {
            try
            {
                conn.Open();
                //sql = "SelectAllSinhVien";
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach(var para in lstPara)
                {
                    cmd.Parameters.AddWithValue(para.key, para.value);
                }
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {

                conn.Close();
            }
        }
        public DataRow Select(string sql)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt.Rows[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load thông tin chi tiết :" + ex.Message);
                return null;

            }
            finally
            {
                conn?.Close();
            }

        }
        public int ExCute(string sql,List<CustomParameter> lstPara)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach(var p in lstPara)
                {
                    cmd.Parameters.AddWithValue(p.key,p.value);
                }
                var rs = cmd.ExecuteNonQuery();
                return (int)rs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thực thi câu lệnh : " + ex.Message);
                return -100;
            }
            finally
            {

            conn?.Close(); 
            }
        }
    }

}
