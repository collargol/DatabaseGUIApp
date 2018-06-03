using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ProjectBasicSQL
{
    public partial class FormAdd : Form
    {
        private enum addedItem
        {
            none,
            Athlete,
            Team,
            Competition,
            Country,
            Arena
        };

        public FormAdd()
        {
            InitializeComponent();
        }

        public void SendAddQuery(String query)
        {
            try
            {
                using (var conn = new SqlConnection(Program.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected virtual void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
