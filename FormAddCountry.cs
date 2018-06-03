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
    public partial class FormAddCountry : FormAdd, IQueriesAdd
    {
        public FormAddCountry() : base()
        {
            this.Text = "FormAddCountry";
            InitializeComponent();
        }

        public string CreateAddQuery()
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("Missing values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new InvalidDataException("No values provided");
            }
            else
            {
                return "insert into Countries values('" + textBox1.Text + "');";
            }
        }

        protected override void button2_Click(object sender, EventArgs e)
        {
            //base.button2_Click(sender, e);
            try
            {
                SendAddQuery(CreateAddQuery());
                textBox1.Text = "";
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
