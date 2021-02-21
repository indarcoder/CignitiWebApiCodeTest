using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestCode.Contract.v1.Requests
{
    public class UserLoginRequest
    {
        [Required( ErrorMessage = "Please send an Username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Please send an Password")]
        public string Password { get; set; }
    }
}
