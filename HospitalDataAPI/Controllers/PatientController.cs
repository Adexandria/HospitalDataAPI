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

        /// <summary>
        /// Get Patients
        /// </summary>
        /// <returns>Patients</returns>
        /// <response code="200">Patients</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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

        /// <summary>
        /// Get Patient by Patient Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <returns>Patient Model</returns>
        /// <response code="200">Patient Model</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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

        /// <summary>
        /// Get Patients Name by Family Name
        /// </summary>
        /// <param name="lastName">Family Name Of The Patient </param>
        /// <returns>Patients</returns>
        /// <response code="200">Patients</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
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

        /// <summary>
        /// Add Patient
        /// </summary>
        /// <param name="newPatient"> Create Model of the Patient Model</param>
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
        /// Update Patient 
        /// </summary>
        /// <remarks>
        ///     </remarks>
        ///     <param name="patientId">Guid Id of the patient</param>
        ///     <param name="updatePatient">Update Model of the Patient Model</param>
        ///     <returns>An updated patient model</returns>
        /// <response code="200">Updated Patient Model</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpPut("{patientId}")]
       
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
