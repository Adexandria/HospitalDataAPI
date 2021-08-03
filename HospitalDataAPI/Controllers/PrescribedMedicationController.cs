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
using System.Linq;
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
