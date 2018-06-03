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
        }
        
        private void actualizePermissions()
        {
            Text += (" [" + Program.Permissions.ToString() + "]");

            switch (Program.Permissions)
            {
                case PermissionLevel.Basic:
                    button1.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;

                    break;
                case PermissionLevel.Manager:

                    break;
                case PermissionLevel.Admin:

                    break;
                default:

                    break;
            }

        }

        private void actualizeDataGridView(String tableName)
        {
            String query = "";
            switch (tableName)
            {
                case "Athletes":
                    query = "select a.athleteID as ID, a.name as NAME, a.heigth as HEIGHT, a.weight as WEIGHT, cast(a.BMI as decimal(18, 2)) as BMI, t.team_name as TEAM " +
                        "from Athletes as a " +
                        "inner join Teams as t " +
                        "on t.teamID = a.team";
                    break;
                case "Arenas":
                    query = "select a.arenaID as ID, a.name as ARENA, c.name as COUNTRY " +
                        "from Arenas as a " +
                        "inner join Countries as c " +
                        "on c.countryID = a.country";
                    break;
                case "Competitions":
                    query = "select c.compID as ID, c.competitions_name as NAME, a.name as ARENA, c.compets_date as DATE " +
                        "from Competitions as c " +
                        "inner join Arenas as a " +
                        "on a.arenaID = c.arena";
                    break;
                case "Countries":
                    query = "select c.countryID as ID, c.name as COUNTRY " +
                        "from Countries as c";
                    break;
                case "Teams":
                    query = "select t.teamID as ID, t.team_name as TEAM, c.name as COUNTRY " +
                        "from Teams as t " +
                        "inner join Countries as c " +
                        "on c.countryID = t.country";
                    break;
                default:
                    MessageBox.Show("Wrong table name passed to method", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            using (var data = new SqlDataAdapter(query, Program.connectionString))
            {
                DataTable dataTable = new DataTable();
                data.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Table not selected in combobox", "Table not selected");
            }
            else
            {
                actualizeDataGridView(comboBox1.SelectedItem.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAddAthlete formAdd = new FormAddAthlete();
            formAdd.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormAddCompetition formAdd = new FormAddCompetition();
            formAdd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormAddCountry formAdd = new FormAddCountry();
            formAdd.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormAddTeam formAdd = new FormAddTeam();
            formAdd.ShowDialog();
        }
    }
}
