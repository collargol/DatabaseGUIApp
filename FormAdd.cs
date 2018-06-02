using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectBasicSQL
{
    public abstract partial class FormAdd : Form
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

        //virtual protected void SendAddQuery() { }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
