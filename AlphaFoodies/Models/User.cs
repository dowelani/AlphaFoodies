using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaFoodies.Models
{
    public class User
    {
        public string Email_Address;
        public string Password;

        public User(string Email_Address,string Password)
        {
            this.Email_Address = Email_Address;
            this.Password = Password;
        }
    }
}