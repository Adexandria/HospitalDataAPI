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
    public class LabResultController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IPatient _patient;
        readonly IResult _result;
        readonly ITest _test;
        public LabResultController(IMapper _mapper, IPatient _patient, IResult _result, ITest _test)
        {
            this._mapper = _mapper;
            this._patient = _patient;
            this._result = _result;
            this._test = _test;
        }

        /// <summary>
        /// Get Patient Lab Results
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <returns> Patient Lab Results</returns>
        /// <response code="200">Patient Lab Results</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet]
        public ActionResult<IEnumerable<LabResultDTO>> GetPatientResults(Guid patientId) 
        {
            try
            {
                Patient currentPatient =  _patient.GetPatientById(patientId).Result;
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }
                IEnumerable<LabResult> patientResults = _result.GetLabResultsById(patientId);
                IEnumerable<LabResultDTO> mappedResults = _mapper.Map<IEnumerable<LabResultDTO>>(patientResults);
                return Ok(mappedResults);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get Patient Lab Result By Lab Test Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="testId"> Lab Result Id</param>
        /// <returns>Patient Lab Results</returns> 
        /// <response code="200">Patient Lab Results</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet("testId")]
        public ActionResult<IEnumerable<LabResultDTO>> GetPatientResultsByTestId(Guid patientId, [FromQuery]Guid testId) 
        {
            try
            {
                Patient currentPatient =  _patient.GetPatientById(patientId).Result;
                if (currentPatient == null)
                { 
                    return NotFound("Patient not found"); 
                }
                LabTest currentLabTest = _test.GetLabTestById(patientId, testId).Result;
                if(currentLabTest == null)
                {
                    return NotFound("Labtest not found");
                }
                IEnumerable<LabResult> patientResult = _result.GetLabResultsByTestId(patientId, testId);
                IEnumerable<LabResultDTO> mappedResult = _mapper.Map<IEnumerable<LabResultDTO>>(patientResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get Patient Lab Result By Lab Result Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="resultId">Lab Result Id</param>
        /// <returns>Patient Lab Result</returns>
        /// <response code="200">Patient Lab Result</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet("{resultId}")]
        public async Task<ActionResult<LabResultDTO>>GetPatientResultById(Guid patientId,Guid resultId)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }
                LabResult patientResult = await _result.GetLabResultById(patientId, resultId);
                LabResultDTO mappedResult = _mapper.Map<LabResultDTO>(patientResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Add Patient Lab Result
        /// </summary>
        /// <param name="patientId">patient id</param>
        /// <param name="labResult">Create Model of Lab Result Model</param>
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
        public async Task<ActionResult> AddPatientResult(Guid patientId,LabResultCreate labResult)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }
                LabTest currentLabTest = await _test.GetLabTestById(patientId, labResult.TestId);
                if (currentLabTest == null)
                {
                    return NotFound("Labtest not found");
                }            
                LabResult newLabResult = _mapper.Map<LabResult>(labResult);
                await _result.AddLabResultId(patientId,newLabResult);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update Patient Lab Result 
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="testId">Test Id</param>
        /// <param name="labResult">Update Model of Lab Result Model</param>
        /// <returns>Updated Patient Lab Result</returns>
        /// <response code="200">Patient Lab Result</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpPut("{testId}")]
        public async Task<ActionResult<LabResultDTO>> UpdatePatientResult(Guid patientId,Guid testId,LabResultUpdate labResult) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }
                LabTest currentLabTest = await _test.GetLabTestById(patientId,testId);
                if (currentLabTest == null)
                {
                    return NotFound("Labtest not found");
                }
                LabResult currentLabResult = await _result.GetLabResultById(patientId,labResult.ResultId);
                if (currentLabResult == null)
                {
                    return NotFound("Lab result doesn't exist");
                }
                LabResult newLabResult = _mapper.Map<LabResult>(labResult);
                LabResult updatedLabResult = await _result.UpdateLabResultId(patientId,newLabResult);
                LabResultDTO mappedResult = _mapper.Map<LabResultDTO>(updatedLabResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete Lab Result By Lab Result Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="resultId">Lab Result Id</param>
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
        [HttpDelete("{resultId}")]
        public async Task<ActionResult> DeletePatientResult(Guid patientId,Guid resultId)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }
                LabResult labResult = await _result.GetLabResultById(patientId,resultId);
                if(labResult == null) 
                {
                    return NotFound("Lab result not found");
                }
                await _result.DeleteLabResultById(patientId, resultId);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
