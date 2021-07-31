using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Model.LabModel;
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
        [HttpGet]
        public ActionResult<IEnumerable<LabTestDTO>> GetPatientTest(Guid patientId ) 
        {
            var tests = _test.GetLabTestsById(patientId);
            var mappedTests = _mapper.Map<IEnumerable<LabTestDTO>>(tests);
            return Ok(mappedTests);
        }
        [HttpGet("{testId}")]
        public async Task<ActionResult<LabTestDTO>> GetTestById(Guid patientId,Guid testId) 
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");
                var currentTest = await _test.GetLabTestById(patientId, testId);
                if (currentTest == null) return NotFound("Test doesn't exist");
                var mappedTest = _mapper.Map<LabTestDTO>(currentTest);
                return Ok(mappedTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("code")]
        public async Task<ActionResult<LabTestDTO>> SearchTestByCode(Guid patientId,string code)
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");
                var currentTest = await _test.GetLabTestByCode(patientId, code);
                if (currentTest == null) return NotFound("Test doesn't exist");
                var mappedTest = _mapper.Map<LabTestDTO>(currentTest);
                return Ok(mappedTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<LabTestDTO>> AddPatientTest(Guid patientId,LabTestCreate labTest) 
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("user not found");
                var newLabTest = _mapper.Map<LabTest>(labTest);
                await _test.AddLabTestById(patientId, newLabTest);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult<LabTestDTO>> UpdatePatientTest(Guid patientId,LabTestUpdate labTest) 
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");

                var currentTest = await _test.GetLabTestById(patientId, labTest.TestId);
                if (currentTest == null) return NotFound("Test doesn't exist");

                var updateLabTest = _mapper.Map<LabTest>(labTest);
                var updatedLabTest = await _test.UpdateLabTestById(patientId, updateLabTest);
                var mappedLabTest = _mapper.Map<LabTestDTO>(updateLabTest);
                return Ok(mappedLabTest);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
