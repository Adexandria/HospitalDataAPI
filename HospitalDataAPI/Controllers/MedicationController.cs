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
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Return if not found",type: typeof(NotFoundResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest,"Return if failed", type: typeof(BadRequestResult))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized,"Return when UnAuthorized", type: typeof(UnauthorizedResult))]

    [Route("api/medication")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MedicationController : ControllerBase
    {
        readonly IMapper _mapper;
        readonly IMedication _medication;
        public MedicationController(IMedication _medication, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._medication = _medication;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MedicationDTO>> GetMedications() 
        {
            try
            {
                IEnumerable<Medication> medications = _medication.GetMedications;
                IEnumerable<MedicationDTO> mappedMedications = _mapper.Map<IEnumerable<MedicationDTO>>(medications);
                return Ok(mappedMedications);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("name")]
        public ActionResult<IEnumerable<MedicationDTO>> GetMedicationByName([FromQuery] string name) 
        {
            try
            {
                IEnumerable<Medication> medication = _medication.GetMedicationByName(name);
                IEnumerable<MedicationDTO> mappedMedication = _mapper.Map<IEnumerable<MedicationDTO>>(medication);
                return Ok(mappedMedication);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{medicationId}")]
        public async Task<ActionResult<MedicationDTO>> GetMedicationById(int medicationId) 
        {
            try
            {
                Medication currentMedication = await _medication.GetMedicationById(medicationId);
                if (currentMedication == null)
                { 
                    return NotFound();
                }
                MedicationDTO mappedMedication = _mapper.Map<MedicationDTO>(currentMedication);
                return Ok(mappedMedication);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
       [HttpPost]
       public async Task<ActionResult> AddMedication(MedicationCreate newMedication)
        {
            try
            {
                Medication mappedmedication = _mapper.Map<Medication>(newMedication);
                await _medication.AddMedication(mappedmedication);
                return Ok("Medication added successfully");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{medicationId}")]
        public async Task<ActionResult<MedicationDTO>> UpdateMedication(MedicationUpdate updateMedication,int medicationId)
        {
            try
            {
                Medication currentMedication = await _medication.GetMedicationById(medicationId);
                if (currentMedication == null)
                {
                    return NotFound();
                }
                Medication mappedMedication = _mapper.Map<Medication>(updateMedication);
                Medication medication = await _medication.UpdateMedication(mappedMedication, medicationId);
                MedicationDTO medicationDTO = _mapper.Map<MedicationDTO>(medication);
                return Ok(medicationDTO);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
        [HttpDelete("{medicationId}")]
        public async Task<ActionResult> DeleteMedication(int medicationId) 
        {
            var currentMedication = _medication.GetMedicationById(medicationId);
            if (currentMedication == null)
            {
                return NotFound();
            }
            await _medication.DeleteMedication(medicationId);
            return Ok("Successful");
        }
    }
}
