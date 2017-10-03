using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SucLib.Core;

namespace MvcApplication.Models
{
    public class Login
    {
        public Login() { }
        private string _login_name;
        private string _password;
        private string _validatecode;


        public string LOGIN_NAME
        {
            set { _login_name = value; }
            get { return _login_name; }
        }
        public string PASSWORD
        {
            set { _password = value; }
            get { return _password; }
        }
        public string VALIDATECODE
        {
            set { _validatecode = value; }
            get { return _validatecode; }
        }
    }
}