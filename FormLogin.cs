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
    public partial class FormLogin : Form
    {
        private LoginHandling loginHandling;

        public FormLogin(Form1 mainForm)
        {
            InitializeComponent();
            loginHandling = new LoginHandling(@"C:\Users\Michał\Documents\Visual Studio 2017\Projects\ProjectBasicSQL\ProjectBasicSQL\users.txt");
            //button2.Click += (s, args) => mainForm.Close();
        }

        // quit button
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // login button 
        private void button1_Click(object sender, EventArgs e)
        {
            // all login stuff below...
            String user = textBox1.Text;
            String password = textBox2.Text;
            if (user.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Empty user or password field!", "Login error");
            }
            else
            {
                if (loginHandling.CheckLoginCorrectness(user, password))
                {
                    Program.Permissions = loginHandling.LoginUser(user);
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong user or password!", "Login error");
                }
            }
        }
    }
}
