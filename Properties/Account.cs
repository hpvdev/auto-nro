using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAuto_v1.Properties
{
    class Account
    {
        string userName;
        string passWord;

        public Account(string userName, string passWord)
        {
            this.UserName = userName;
            this.PassWord = passWord;
        }

        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string UserName1 { get => userName; set => userName = value; }
        public string PassWord1 { get => passWord; set => passWord = value; }
    }
}
