using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscribeManagement.WebAPI.Models.Auth
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
