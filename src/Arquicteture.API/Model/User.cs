using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arquicteture.API.Model
{
    public class User : Notify
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User()
        {

        }
        public User(string name, string username, string password)
        {
            if (string.IsNullOrEmpty(name)) AddNotify("Name is required");
            Name = name;

            if (string.IsNullOrEmpty(username)) AddNotify("Username is required");
            Username = username;

            if (string.IsNullOrEmpty(password)) AddNotify("Password is required");
            Password = password;
        }
    }

  
}
