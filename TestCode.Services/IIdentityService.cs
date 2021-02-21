using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCode.Domain;

namespace TestCode.Services
{
    public interface IIdentityService
    {       
        Task<AuthenticationResult> LoginAsync(string Username, string Password);       
    }
}
