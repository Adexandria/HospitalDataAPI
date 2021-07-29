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
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return if not found")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]

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
                var patients = _patient.GetPatients;
                var mappedPatients = _mapper.Map<IEnumerable<PatientsDTO>>(patients);
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
                var patient = await _patient.GetPatientById(patientId);
                if (patient == null) return NotFound("Patient doesn't exist");
                var mappedPatient = _mapper.Map<PatientDTO>(patient);
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
                var currentPatients = _patient.GetPatientByName(lastName);
                var mappedPatient = _mapper.Map<IEnumerable<PatientsDTO>>(currentPatients);
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
                var mappedPatient = _mapper.Map<Patient>(newPatient);
                await _patient.AddPatient(mappedPatient);
                return Ok("Patient successfully added");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{patientId}")]
        public async Task<ActionResult<PatientDTO>> UpdatePatient(Guid patientId,PatientUpdate updatePatient)
        {
            try
            {
                var currentPatient = await _patient.GetPatientById(patientId);
                if (currentPatient == null) return NotFound();
                var mappedPatient = _mapper.Map<Patient>(updatePatient);
                var patient = await _patient.UpdatePatient(mappedPatient,patientId);
                var currentPatientDTO = _mapper.Map<PatientDTO>(patient);
                return Ok(currentPatientDTO);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
