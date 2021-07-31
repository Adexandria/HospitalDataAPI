using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;


namespace HospitalDataAPI.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK, "Return if sucessful")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Return when UnAuthorized", type: typeof(UnauthorizedResult))]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibraryController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly ITest _test;
        public LibraryController(IMapper _mapper,ITest _test)
        {
            this._test = _test;
            this._mapper = _mapper;
        }

        /// <summary>
        /// Get Lab Test Codes
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <returns>All Lab Test Codes in the database</returns>
        /// <response code="200">All Lab Test Codes in the database</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("codings")]
        public ActionResult<IEnumerable<CodingDTO>> GetLabTestCodes() 
        {
            var codes = _test.GetCodings;
            var mappedCodes = _mapper.Map<IEnumerable<CodingDTO>>(codes);
            return Ok(mappedCodes);
        }

        /// <summary>
        /// Get Lab Test Codes By Name
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <returns>searched Lab Test Codes in the database</returns>
        /// <response code="200">searched Lab Test Codes in the database</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("codings/name")]
        public ActionResult<IEnumerable<CodingDTO>> GetLabTestCodesByName([FromQuery]string name)
        {
            var codes = _test.GetCodingsByName(name);
            var mappedCodes = _mapper.Map<IEnumerable<CodingDTO>>(codes);
            return Ok(mappedCodes);
        }

        /// <summary>
        /// Get Lab Test Category
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <returns>All Lab Test Categories in the database</returns>
        /// <response code="200">All Lab Test Categories in the database</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategoryDTO>> GetLabTestCategory()
        {
            var categories = _test.GetCategories;
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(mappedCategories);
        }

        /// <summary>
        /// Get Lab Test Category by Name
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <returns>searched Lab Test Categories in the database</returns>
        /// <response code="200">searched Lab Test Categories in the database</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [HttpGet("categories/name")]
        public ActionResult<IEnumerable<CategoryDTO>> GetLabTestCategoryByName([FromQuery] string name)
        {
            var categories = _test.GetCategoryByName(name);
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(mappedCategories);
        }
    }
   
}
