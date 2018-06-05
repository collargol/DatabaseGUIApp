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
    public partial class FormAddAthlete : FormAdd, IQueriesAdd
    {
        public FormAddAthlete(Form1 parent) : base(parent)
        {
            this.Text = "FormAddAthlete";
            InitializeComponent();
            // filling combobox
            using (var conn = new SqlConnection(Program.connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Teams", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //MessageBox.Show(reader[0].ToString() + " " + reader[1].ToString());
                        comboBox1.Items.Add(reader[1].ToString());
                    }
                }
            }
        }

        public string CreateAddQuery()
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Missing values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("No values provided");
            }
            else
            {
                String teamID = "";
                // get ID of selected team
                using (var conn = new SqlConnection(Program.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Teams where name = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        teamID = reader[0].ToString();
                    }
                }
                //
                return "insert into Athletes(name, team, heigth, weight) values('" + textBox1.Text + "', " + teamID + ", " + textBox2.Text + ", " + textBox3.Text + ");";
            }
            //insert into Athletes(name, team, heigth, weight) values('Ricky', 1, 1.80, 105);
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            //base.button2_Click(sender, e);
            try
            {
                SendAddQuery(CreateAddQuery());
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.SelectedItem = null;
                parentForm.actualizeDataGridView("Athletes");
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
