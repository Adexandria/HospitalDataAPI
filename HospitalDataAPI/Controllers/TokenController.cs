using System;
using HospitalDataAPI.Model.DTO;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

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
        /// <summary>
        /// Get AccessToken
        /// </summary>
        /// <returns>Access Token</returns>
        /// <response code="200">Token Model</response>
        /// <response code = "400"> Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<TokenDTO> GetAccessToken() 
        {
            try
            {
                SigningCredentials signingCredentials = _credential.GetSigningCredentials();
                JwtSecurityToken tokenOptions = _credential.GenerateTokenOptions(signingCredentials);
                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                TokenDTO accessToken = new TokenDTO() { AccessToken = token };
                return Ok(accessToken);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }
    }
}
