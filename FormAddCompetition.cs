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
    public partial class FormAddCompetition : FormAdd, IQueriesAdd
    {
        public FormAddCompetition(Form1 parent) : base(parent)
        {
            this.Text = "FormAddCompetition";
            InitializeComponent();
            // filling combobox
            using (var conn = new SqlConnection(Program.connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Arenas", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[1].ToString());
                    }
                }
            }
        }

        public string CreateAddQuery()
        {
            if (textBox1.Text.Equals("") || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Missing values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("No values provided");
            }
            else
            {
                String date = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                String arenaID = "";
                // get ID of selected arena
                using (var conn = new SqlConnection(Program.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Arenas where name = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        arenaID = reader[0].ToString();
                    }
                }
                //
                return "insert into Competitions values('" + textBox1.Text + "', " + arenaID + ", '" + date + "');";
            }
            // insert into Competitions values ('World Cup', 1, '20190819');
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            //base.button2_Click(sender, e);
            try
            {
                SendAddQuery(CreateAddQuery());
                textBox1.Text = "";
                comboBox1.SelectedItem = null;
                parentForm.actualizeDataGridView("Competitions");
                if (parentForm.closingAddForms)
                {
                    Close();
                }
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (InvalidDataException exc)
            {
                MessageBox.Show(exc.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
