using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ProjectBasicSQL
{
    public enum PermissionLevel
    {
        None,
        Basic,
        Manager,
        Admin
    };

    class LoginHandling
    {
        public struct LoginData
        {
            public String User { get; set; }
            public String Password { get; set; }
            public PermissionLevel Permissions { get; set; }
        }

        public List<LoginData> LoginDatas { get; private set; }

        public LoginHandling(String filePath)
        {
            LoginDatas = new List<LoginData>();
            try
            {
                String[] lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i++)
                {
                    String[] splittedLine = lines[i].Split(' ');
                    switch (splittedLine[2])
                    {
                        case "a":
                            LoginDatas.Add(new LoginData { User = splittedLine[0], Password = splittedLine[1], Permissions = PermissionLevel.Admin });
                            break;
                        case "m":
                            LoginDatas.Add(new LoginData { User = splittedLine[0], Password = splittedLine[1], Permissions = PermissionLevel.Manager });
                            break;
                        case "b":
                            LoginDatas.Add(new LoginData { User = splittedLine[0], Password = splittedLine[1], Permissions = PermissionLevel.Basic });
                            break;
                        default:
                            LoginDatas.Add(new LoginData { User = splittedLine[0], Password = splittedLine[1], Permissions = PermissionLevel.Basic });
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException || e is DirectoryNotFoundException)
                {
                    MessageBox.Show(e.Message, "Exception while reading file");
                }
                Application.Exit();
            }
        }

        public bool CheckLoginCorrectness(String user, String password)
        {
            if (LoginDatas.Exists(x => x.User.Equals(user)))
            {
                LoginData record = LoginDatas.Find(x => x.User.Equals(user));
                return record.Password.Equals(password);
            }
            else
            {
                return false;
            }
        }

        public PermissionLevel LoginUser(String user)
        {
            return LoginDatas.Find(x => x.User.Equals(user)).Permissions;
        }
    }
}
