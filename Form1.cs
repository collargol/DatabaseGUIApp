﻿using System;
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
            closingAddForms = false;
            editItemQuery = "";
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
                    button8.Enabled = false;
                    break;
                case PermissionLevel.Manager:
                    button8.Enabled = false;
                    break;
                case PermissionLevel.Admin:

                    break;
                default:

                    break;
            }

        }

        public void actualizeDataGridView(String tableName)
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

        private String CreateDeleteQueryOnIndex()
        {
            String currentTable = comboBox1.SelectedItem.ToString();
            using (var conn = new SqlConnection(Program.connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select column_name from information_schema.columns where table_name = '" + currentTable + "'", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    return "delete from " + currentTable + " where " + reader[0].ToString() + " = ";
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();   
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

        // add forms

        private void button1_Click(object sender, EventArgs e)
        {
            FormAddArena formAdd = new FormAddArena(this);
            formAdd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAddAthlete formAdd = new FormAddAthlete(this);
            formAdd.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormAddCompetition formAdd = new FormAddCompetition(this);
            formAdd.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FormAddCountry formAdd = new FormAddCountry(this);
            formAdd.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormAddTeam formAdd = new FormAddTeam(this);
            formAdd.ShowDialog();
        }

        // delete item

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null || dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Nothing selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                List<string> rowIndexesToDelete = new List<string>();
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (!rowIndexesToDelete.Contains(cell.RowIndex.ToString()))
                    {
                        rowIndexesToDelete.Add(dataGridView1.Rows[cell.RowIndex].Cells[0].Value.ToString());
                    }
                }
                try
                {
                    using (var conn = new SqlConnection(Program.connectionString))
                    {
                        conn.Open();
                        foreach (String index in rowIndexesToDelete)
                        {
                            String query = CreateDeleteQueryOnIndex() + index;
                            SqlCommand command = new SqlCommand(query, conn);
                            command.ExecuteNonQuery();
                        }
                    }
                    actualizeDataGridView(comboBox1.SelectedItem.ToString());
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Deleting failed. One or more deletions terminated because of existing references.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // actualize options 

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            closingAddForms = checkBox1.Checked;
        }

        // edit item
        
        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("No cell selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
                //List<string> rowIndexesToEdit = new List<string>();
                //foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                //{
                //    if (!rowIndexesToEdit.Contains(cell.RowIndex.ToString()))
                //    {
                //        rowIndexesToEdit.Add(dataGridView1.Rows[cell.RowIndex].Cells[0].Value.ToString());
                //    }
                //}
                for (int i = 0; i < dataGridView1.SelectedCells.Count; i++)
                {
                    if (i > 0 && (dataGridView1.SelectedCells[i].RowIndex != dataGridView1.SelectedCells[i - 1].RowIndex))
                    {
                        MessageBox.Show("More than one item selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                String indexToEdit = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                String tableName = comboBox1.SelectedItem.ToString();
                // open form to edit
                OpenEditForm(tableName);
                //
                if (editItemQuery.Equals(""))
                {
                    MessageBox.Show("Edit query is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    try
                    {
                        using (var conn = new SqlConnection(Program.connectionString))
                        {
                            conn.Open();
                            //
                            SqlCommand commandDelete = new SqlCommand(CreateDeleteQueryOnIndex() + indexToEdit, conn);
                            commandDelete.ExecuteNonQuery();
                            //
                            SqlCommand commandOn = new SqlCommand("set identity_insert " + tableName + " on", conn);
                            commandOn.ExecuteNonQuery();
                            //
                            SqlCommand commandInsert = new SqlCommand(editItemQuery.Replace("?", indexToEdit), conn);
                            commandInsert.ExecuteNonQuery();
                            //
                            SqlCommand commandOff = new SqlCommand("set identity_insert " + tableName + " off", conn);
                            commandOff.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException)
                    {
                        MessageBox.Show("Item cannot be edited due to existed references", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            
            }
            
        }

        private void OpenEditForm(String tableName)
        {
            switch (tableName)
            {
                case "Athletes":
                    FormAddAthlete formAddAthlete = new FormAddAthlete(this);
                    formAddAthlete.ButtonName = "Edit";
                    formAddAthlete.ShowDialog();
                    break;
                case "Arenas":
                    FormAddArena formAddArena = new FormAddArena(this);
                    formAddArena.ButtonName = "Edit";
                    formAddArena.ShowDialog();
                    break;
                case "Competitions":
                    FormAddCompetition formAddCompetition = new FormAddCompetition(this);
                    formAddCompetition.ButtonName = "Edit";
                    formAddCompetition.ShowDialog();
                    break;
                case "Countries":
                    FormAddCountry formAddCountry = new FormAddCountry(this);
                    formAddCountry.ButtonName = "Edit";
                    formAddCountry.ShowDialog();
                    break;
                case "Teams":
                    FormAddTeam formAddTeam = new FormAddTeam(this);
                    formAddTeam.ButtonName = "Edit";
                    formAddTeam.ShowDialog();
                    break;
                default:
                    MessageBox.Show("Wrong table name in combobox selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
