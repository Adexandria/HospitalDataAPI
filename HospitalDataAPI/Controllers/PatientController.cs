using AutoMapper;
using HospitalDataAPI.Model.DTO.Patients;
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

    [Route("api/patient")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IPatient _patient;
        public PatientController(IMapper _mapper, IPatient _patient)
        {
            this._mapper = _mapper;
            this._patient = _patient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PatientsDTO>> GetPatients()
        {
            try
            {
                IEnumerable<Patient> patients = _patient.GetPatients;
                IEnumerable<PatientsDTO> mappedPatients = _mapper.Map<IEnumerable<PatientsDTO>>(patients);
                return Ok(mappedPatients);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpGet("{patientId}")]
        public async Task<ActionResult<PatientDTO>> GetPatient(Guid patientId)
        {
            try
            {
                Patient patient = await _patient.GetPatientById(patientId);
                if (patient == null)
                {
                    return NotFound("Patient doesn't exist");
                }
                PatientDTO mappedPatient = _mapper.Map<PatientDTO>(patient);
                return Ok(mappedPatient);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("Lastname")]
        public ActionResult<IEnumerable<PatientDTO>> GetPatientByName([FromQueryAttribute] string lastName)
        {
            try
            {
                IEnumerable<Patient> currentPatients = _patient.GetPatientByName(lastName);
                IEnumerable<PatientsDTO> mappedPatient = _mapper.Map<IEnumerable<PatientsDTO>>(currentPatients);
                return Ok(mappedPatient);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddPatient(PatientCreate newPatient) 
        {
            try
            {
                Patient mappedPatient = _mapper.Map<Patient>(newPatient);
                await _patient.AddPatient(mappedPatient);
                return Ok("Patient successfully added");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update a patient Database.
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <param name="patientId">Guid Id of the patient</param>
        ///     <param name="updatePatient">Update Model of the patient Model</param>
        ///     <returns>An updated patient model</returns>
        /// <response code="200">An updated patient model</response>
        /// <response code="404">If the item is null</response>   
        [HttpPut("{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]            
        [Produces("application/json")]
        public async Task<ActionResult<PatientDTO>> UpdatePatient(Guid patientId,PatientUpdate updatePatient)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) {
                    return NotFound();
                }
                Patient mappedPatient = _mapper.Map<Patient>(updatePatient);
                Patient patient = await _patient.UpdatePatient(mappedPatient,patientId);
                PatientDTO currentPatientDTO = _mapper.Map<PatientDTO>(patient);
                return Ok(currentPatientDTO);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
