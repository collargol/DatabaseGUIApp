﻿using System;
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
    public partial class FormAdd : Form
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

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}