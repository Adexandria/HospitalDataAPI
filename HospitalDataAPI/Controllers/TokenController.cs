using System;
using HospitalDataAPI.Model.DTO;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace HospitalDataAPI.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK, "Return if sucessful",type:typeof(TokenDTO))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Return if failed", type: typeof(BadRequestResult))]
    [Route("api/token")]
    [ApiController]
    [AllowAnonymous]

    public class TokenController : ControllerBase
    {
        readonly Credentials _credential;
       
        public TokenController(Credentials _credential)
        {
            this._credential = _credential;
        }

        [HttpGet]
        public ActionResult<TokenDTO> AccessToken() 
        {
            try
            {
                var signingCredentials = _credential.GetSigningCredentials();
                var tokenOptions = _credential.GenerateTokenOptions(signingCredentials);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                var accessToken = new TokenDTO() { AccessToken = token };
                return Ok(accessToken);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}
