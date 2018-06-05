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
    public partial class FormAddArena : FormAdd, IQueriesAdd
    {
        public FormAddArena(Form1 parent) : base(parent)
        {
            this.Text = "FormAddArena";
            InitializeComponent();
            // filling combobox
            using (var conn = new SqlConnection(Program.connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand("select * from Countries", conn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader[1].ToString());
                    }
                }
            }

        }

        public String CreateAddQuery()
        {
            if (textBox1.Text.Equals("") || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Missing values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("No values provided");
            }
            else
            {
                String countryID = "";
                // get ID of selected country
                using (var conn = new SqlConnection(Program.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Countries where name = '" + comboBox1.SelectedItem.ToString() + "'", conn);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        countryID = reader[0].ToString();
                    }
                }
                //
                return "insert into Arenas values ('" + textBox1.Text + "', " + countryID + ");";
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            //base.button2_Click(sender, e);
            try
            {
                SendAddQuery(CreateAddQuery());
                textBox1.Text = "";
                comboBox1.SelectedItem = null;
                parentForm.actualizeDataGridView("Arenas");
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
