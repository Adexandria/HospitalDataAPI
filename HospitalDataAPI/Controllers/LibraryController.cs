using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HospitalDataAPI.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK, "Return if sucessful")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return if not found")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
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
        [HttpGet("codings")]
        public ActionResult<IEnumerable<CodingDTO>> GetLabTestCodes() 
        {
            var codes = _test.GetCodings;
            var mappedCodes = _mapper.Map<IEnumerable<CodingDTO>>(codes);
            return Ok(mappedCodes);
        }
        [HttpGet("codings/name")]
        public ActionResult<IEnumerable<CodingDTO>> GetLabTestCodesByName([FromQuery]string name)
        {
            var codes = _test.GetCodingsByName(name);
            var mappedCodes = _mapper.Map<IEnumerable<CodingDTO>>(codes);
            return Ok(mappedCodes);
        }
        [HttpGet("categories")]
        public ActionResult<IEnumerable<CategoryDTO>> GetLabTestCategory()
        {
            var categories = _test.GetCategories;
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(mappedCategories);
        }
        [HttpGet("categories/name")]
        public ActionResult<IEnumerable<CategoryDTO>> GetLabTestCategoryByName([FromQuery] string name)
        {
            var categories = _test.GetCategoryByName(name);
            var mappedCategories = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(mappedCategories);
        }
    }
   
}
