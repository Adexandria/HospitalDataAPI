using AutoMapper;
using HospitalDataAPI.Model.DTO.Patients;
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
    [SwaggerResponse((int)HttpStatusCode.OK, "Returns if sucessful")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Returns if not found")]
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
    }
}
