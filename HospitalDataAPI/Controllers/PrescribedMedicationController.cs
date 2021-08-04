using AutoMapper;
using HospitalDataAPI.Model.DTO.Medications;
using HospitalDataAPI.Model.MedicationModel;
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
    public class PrescribedMedicationController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IMedication _medication;
        readonly IPatient _patient;
        public PrescribedMedicationController(IMapper _mapper, IMedication _medication, IPatient _patient)
        {
            this._mapper = _mapper;
            this._medication = _medication;
            this._patient = _patient;
        }
        /// <summary>
        /// Get Patient Prescribed Medications
        /// </summary>
        /// <param name="patientId"> Patient Id</param>
        /// <returns>Prescribed Medications</returns>
        /// <response code="200">Prescribed Medications </response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrescribedMedsDTO>>> GetPatientMeds(Guid patientId) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient not found");
                }

                IEnumerable<PrescribedMedication> currentMeds = _medication.GetMedicationsById(patientId);
                IEnumerable<PrescribedMedsDTO> mappedMeds = _mapper.Map<IEnumerable<PrescribedMedsDTO>>(currentMeds);
                return Ok(mappedMeds);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            
        }

        /// <summary>
        /// Get Patient Prescribed Medication by Id
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="prescribedId">Prescribed Medication Id</param>
        /// <returns>Prescribed Medication Model</returns>
        /// <response code="200">Prescribed Medication Model</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpGet("{prescribedId}")]
        public async Task<ActionResult<PrescribedMedsDTO>> GetPatientMedById(Guid patientId,Guid prescribedId)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                { 
                    return 
                    NotFound("Patient not found"); 
                }

                PrescribedMedication currentMeds = await _medication.GetMedicationById(patientId,prescribedId);
                if (currentMeds == null)
                { 
                    return NotFound();
                }
                PrescribedMedsDTO mappedMeds = _mapper.Map<PrescribedMedsDTO>(currentMeds);
                return Ok(mappedMeds);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Add Patient Prescribed Medication
        /// </summary>
        /// <param name="patientId">Patient Model</param>
        /// <param name="patientMedication">Create Model for Prescribed Medication Model</param>
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
        public async Task<ActionResult> AddPatientMedication (Guid patientId,PrescribedMedsCreate patientMedication) 
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) 
                { 
                    return NotFound("Patient not found"); 
                }
                Medication medication = await _medication.GetMedicationById(patientMedication.MedicationId);
                if(medication == null)
                {
                    return NotFound("Medication doesn't exist");
                }
                PrescribedMedication mappedMedication = _mapper.Map<PrescribedMedication>(patientMedication);
                await _medication.AddMedicationById(patientId, mappedMedication);
                return Ok("Successful");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Update Patient  Prescribed Medication
        /// </summary>
        /// <param name="patientId">Patient Id</param>
        /// <param name="patientMedication">Update Model of Prescribed Medication</param>
        /// <returns> Prescribed Medication </returns>
        /// <response code="200">Updated Patient Medication Model</response>
        /// <response code = "400"> Bad request</response>
        /// <response code="404">Not Found</response>
        /// <response code="401">Unauthorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Produces("application/json")]
        [HttpPut]
        public async Task<ActionResult<PrescribedMedsDTO>> UpdatePatientmedication (Guid patientId,PrescribedMedsUpdate patientMedication)
        {
            try
            {
                Patient currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null)
                {
                    return NotFound("Patient doesn't exist");
                }
                Medication currentMedication = await _medication.GetMedicationById(patientMedication.MedicationId);
                if (currentMedication == null)
                {
                    return NotFound("Medication doesn't exist");
                }
                PrescribedMedication currentPrescribedMedication = await _medication.GetMedicationById(patientId, patientMedication.PrescribedId);
                if (currentPrescribedMedication == null)
                {
                    return NotFound("Prescribed Medication doesn't exist");
                }
                PrescribedMedication mappedmedication = _mapper.Map<PrescribedMedication>(patientMedication);
                PrescribedMedication medication = await _medication.UpdateMedicationById(patientId, mappedmedication);
                PrescribedMedsDTO mappedPrescription = _mapper.Map<PrescribedMedsDTO>(medication);
                return Ok(mappedPrescription);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
