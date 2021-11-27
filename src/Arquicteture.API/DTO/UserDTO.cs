using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arquicteture.API.DTO
{
    public class UserCreateDTO
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }


    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
