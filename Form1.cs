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

namespace ProjectBasicSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormLogin formLogin = new FormLogin(this);
            formLogin.ShowDialog();
            actualizePermissions();

            using (var data = new SqlDataAdapter("SELECT * FROM Athletes", "Data Source=MICHAL\\SQLEXPRESS01; " +
                                                                        "Initial Catalog=homeworkDB;" +
                                                                        "User id=MICHAL\\Michal;" +
                                                                        "Trusted_Connection=true"))
            {
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }



        }
        
        private void actualizePermissions()
        {
            Text += (" [" + Program.Permissions.ToString() + "]");

            switch (Program.Permissions)
            {
                case PermissionLevel.Basic:
                    button1.Enabled = false;

                    break;
                case PermissionLevel.Manager:

                    break;
                case PermissionLevel.Admin:

                    break;
                default:

                    break;
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormAddArena formAdd = new FormAddArena();
            formAdd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();    // form closes
        }
    }
}
