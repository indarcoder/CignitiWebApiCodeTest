using System;
using System.Collections.Generic;
using System.Text;

namespace TestCode.Domain
{
    public class UserLoginResult
    {
        public string Status { get; set; } = "failed";
        public string Errors { get; set; }
        public int ErrorCode { get; set; } = 0;
    }

    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Errors { get; set; }       
    }
}
