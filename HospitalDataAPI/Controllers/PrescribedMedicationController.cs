using AutoMapper;
using HospitalDataAPI.Model.DTO.Medications;
using HospitalDataAPI.Model.MedicationModel;
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
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");
                var currentMeds = _medication.GetMedicationsById(patientId);
                var mappedMeds = _mapper.Map<IEnumerable<PrescribedMedsDTO>>(currentMeds);
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
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");
                var currentMeds = await _medication.GetMedicationById(patientId,prescribedId);
                if (currentMeds == null) return NotFound();
                var mappedMeds = _mapper.Map<PrescribedMedsDTO>(currentMeds);
                return Ok(mappedMeds);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpPost]
        public async Task<ActionResult<PrescribedMedsDTO>> AddPatientMedication (Guid patientId,PrescribedMedsCreate patientMedication) 
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient not found");
                var mappedMedication = _mapper.Map<PrescribedMedication>(patientMedication);
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
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound("Patient doesn't exist");
                var currentMedication = await _medication.GetMedicationById(patientId, patientMedication.PrescribedId);
                if (currentMedication == null) return NotFound("Prescribed Medication doesn't exist");
                var mappedmedication = _mapper.Map<PrescribedMedication>(patientMedication);
                var medication = await _medication.UpdateMedicationById(patientId, mappedmedication);
                var mappedPrescription = _mapper.Map<PrescribedMedsDTO>(medication);
                return Ok(mappedPrescription);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
