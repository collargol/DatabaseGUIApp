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
    public partial class FormDisplay : Form
    {
        private DataGridView dataGridViewResult = new DataGridView();
        private BindingSource bindingSourceResult = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private Button reloadButton = new Button();
        private Button submitButton = new Button();


        public FormDisplay()
        {
            InitializeComponent();
        }
    }
}
