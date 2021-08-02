using AutoMapper;
using HospitalDataAPI.Model.DTO.LabDTO;
using HospitalDataAPI.Model.LabModel;
using HospitalDataAPI.Model.PatientModel;
using HospitalDataAPI.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        public LabResultController(IMapper _mapper, IPatient _patient, IResult _result)
        {
            this._mapper = _mapper;
            this._patient = _patient;
            this._result = _result;
        }

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
        [HttpGet("{testId}")]
        public async Task<ActionResult<LabResultDTO>> SearchPatientResultByTestId(Guid patientId, Guid testId) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                { 
                    return NotFound("Patient not found"); 
                }
                LabResult patientResult = await _result.GetLabResult(patientId, testId);
                LabResultDTO mappedResult = _mapper.Map<LabResultDTO>(patientResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
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
                LabResult newLabResult = _mapper.Map<LabResult>(labResult);
                await _result.AddLabResultId(patientId,newLabResult);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{testId}")]
        public async Task<ActionResult<LabResultDTO>> UpdatePatientResult(Guid patientId,Guid testId,LabResultUpdate labResult) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) {
                    return NotFound("Patient not found");
                }
                LabResult currentLabResult = await _result.GetLabResult(patientId, testId);
                if (currentLabResult == null)
                {
                    return NotFound("Lab result doesn't exist");
                }
                LabResult newLabResult = _mapper.Map<LabResult>(labResult);
                LabResult updatedLabResult = await _result.UpdateLabResultId(patientId,testId,newLabResult);
                LabResultDTO mappedResult = _mapper.Map<LabResultDTO>(updatedLabResult);
                return Ok(mappedResult);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
