using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Model.LabModel;
using HospitalDataAPI.Model.PatientModel;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HospitalDataAPI.Controllers
{
    [SwaggerResponse((int)HttpStatusCode.OK, "Return if sucessful")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return if not found", type: typeof(NotFoundResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Return if failed", type: typeof(BadRequestResult))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Return when UnAuthorized", type: typeof(UnauthorizedResult))]

    [Route("api/{patientId}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LabTestController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IPatient _patient;
        readonly ITest _test;
        public LabTestController(IMapper _mapper,ITest _test, IPatient _patient)
        {
            this._mapper = _mapper;
            this._test = _test;
            this._patient = _patient;
        }

        /// <summary>
        /// Get Patient Lab Tests
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <returns>Patient Lab Tests</returns>
        /// <response code="200">Patient Lab Tests</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<IEnumerable<LabTestDTO>> GetPatientTests(Guid patientId) 
        {
            try
            {
                Patient currentPatient = _patient.GetPatientById(patientId).Result;
                if (currentPatient == null) 
                {
                    return NotFound("Patient not found");
                }
                IEnumerable<LabTest> tests = _test.GetLabTestsById(patientId);
                IEnumerable<LabTestDTO> mappedTests = _mapper.Map<IEnumerable<LabTestDTO>>(tests);
                return Ok(mappedTests);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Get Lab Test By Lab Test Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="testId">Lab Test Id</param>
        /// <returns>Patient Lab test</returns>
        /// <response code="200">Patient Lab Test</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet("{testId}")]
        public async Task<ActionResult<LabTestDTO>> GetTestById(Guid patientId,Guid testId) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) 
                {
                    return NotFound("Patient not found");
                }
                LabTest currentTest = await _test.GetLabTestById(patientId, testId);
                if (currentTest == null)
                {
                    return NotFound("Test doesn't exist");
                }
                LabTestDTO mappedTest = _mapper.Map<LabTestDTO>(currentTest);
                return Ok(mappedTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Lab Tests By Lab Code
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="code">Lab Code</param>
        /// <returns>Patient Lab Tests</returns>     
        /// <response code="200">Patient Lab Tests</response>
        /// <response code = "400">Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet("code")]
        public ActionResult<IEnumerable<LabTestDTO>> GetTestsByCode(Guid patientId,string code)
        {
            try
            {
                Patient currentPatient = _patient.GetPatientById(patientId).Result;
                if (currentPatient == null) 
                {
                    return NotFound("Patient not found");
                }
                IEnumerable<LabTest> currentTest = _test.GetLabTestByCode(patientId, code);
                if (currentTest == null)
                {
                    return NotFound("Test doesn't exist");
                }
                IEnumerable<LabTestDTO> mappedTest = _mapper.Map<IEnumerable<LabTestDTO>>(currentTest);
                return Ok(mappedTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add Patient Lab Test
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="labTest">Lab Test Create model</param> 
        /// <returns>Successful</returns>
        /// <response code="200">Successful</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<ActionResult> AddPatientTest(Guid patientId,LabTestCreate labTest) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("user not found");
                }
                Coding labCode = await _test.GetCodingById(labTest.CodeId);
                if(labCode == null)
                {
                    return NotFound("Lab code doesn't exist");
                }
                Category labCategory = await _test.GetCategoryById(labTest.CategoryId);
                if(labCategory == null)
                {
                    return NotFound("Lab category doesn't exist");
                }
                LabTest newLabTest = _mapper.Map<LabTest>(labTest);
                await _test.AddLabTestById(patientId, newLabTest);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Update Patient Lab Test
        /// </summary>
        /// <param name="patientId">patient Id</param>
        /// <param name="labTest">Update Model for Patient Model</param>
        /// <returns>Patient Lab Test</returns>
        /// <response code="200">Patient Lab Test</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpPut]
        public async Task<ActionResult<LabTestDTO>> UpdatePatientTest(Guid patientId,LabTestUpdate labTest) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) 
                {
                    return NotFound("Patient not found");
                }

                LabTest currentTest = await _test.GetLabTestById(patientId, labTest.TestId);
                if (currentTest == null)
                {
                    return NotFound("Test doesn't exist");
                }

                LabTest updateLabTest = _mapper.Map<LabTest>(labTest);
                LabTest updatedLabTest = await _test.UpdateLabTestById(patientId, updateLabTest);
                LabTestDTO mappedLabTest = _mapper.Map<LabTestDTO>(updatedLabTest);
                return Ok(mappedLabTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
      
    }
}
