using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TestCode.Contract.v1;
using TestCode.Contract.v1.Requests;
using TestCode.Contract.v1.Responses;
using TestCode.Services;

namespace TestCode.API.Controllers.v1
{
    
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { error = "object is null" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { error = "Invalid object" });
                }

                var userLoginResult = await _identityService.LoginAsync(model.Username, model.Password);
                if (!userLoginResult.Success)
                {
                    return BadRequest(userLoginResult);
                }

                return Ok(userLoginResult);

            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Something went wrong" });
            }

            
        } 
        
    }  
}
