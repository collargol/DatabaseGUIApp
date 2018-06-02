using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjectBasicSQL
{
    class SQLDataHandling //: IDisposable
    {
        private String connectionString;
        //private SqlConnection sqlConnection;

        public SQLDataHandling(String username, String password, String server, String database, bool trustedConnection, uint timeout)
        {
            String isTrusted = trustedConnection ? "yes" : "no";
            connectionString = "user id=" + username +
                ";password=" + password +
                ";server=" + server +
                ";Trusted_Connection=" + isTrusted +
                ";database=" + database +
                ";connection timeout=" + timeout.ToString();
            //sqlConnection = new SqlConnection(connectionString);
            //try
            //{
            //    sqlConnection.Open();
            //}
            //catch (SqlException)
            //{
            //    throw;
            //}
        }

        public void SendQuery(String query)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Data Source=MICHAL\\SQLEXPRESS01;" +
                                                "Initial Catalog=homeworkDB;" +
                                                "User id=MICHAL\\Michal;";
                //"Password=Secret;";
                connection.Open();
                SqlCommand command = new SqlCommand("insert into Countries values('Romania')", connection);
                command.ExecuteNonQuery();
            }

            //SqlCommand command = new SqlCommand(query, sqlConnection);
            //command.ExecuteNonQuery();
        }

        //public void GetData(String command)
        //{
        //    // SqlDataAdapter dataAdapter = new SqlDataAdapter(command, sqlConnection);
        //    //SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        //    DataTable dataTable = new DataTable();
        //    dataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
        //    //dataAdapter.Fill(dataTable);
        //    // these should be get as args to function
        //    //bindingSource1.DataSource = dataTable;
        //    //dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        //}

        //public void Dispose()
        //{
        //    sqlConnection.Close();
        //}
        
    }
}
