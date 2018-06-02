using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectBasicSQL
{
    static class Program
    {

        static public PermissionLevel Permissions = PermissionLevel.None;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //SQLDataHandling sqlDataHandling = new SQLDataHandling(@"MICHAL\Michal", "", @"MICHAL\SQLEXPRESS01", "homeworkDB", false, 20);
            //sqlDataHandling.SendQuery("insert into Countries values ('Romania')");
        }
    }
}
