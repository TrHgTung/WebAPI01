using AutoMapper;
using WebAPI01.Dto;
using WebAPI01.Interfaces;
using WebAPI01.Models;
using WebAPI01.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            return Ok(department);

        }

        [HttpGet("/teachers/{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Department))]
        public IActionResult GetDepartmentOfATeacher(int teacherId)
        {
            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartmentByTeacher(teacherId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(department);
            }
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartment([FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);

            var department = _departmentRepository.GetDepartments().Where(c => c.Name.Trim().ToUpper() == departmentCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (department != null)
            {
                ModelState.AddModelError("", "This department already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving..");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }


        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int departmentId, [FromBody] DepartmentDto updatedDepartment)
        {
            if (updatedDepartment == null)
                return BadRequest(ModelState);

            if (departmentId != updatedDepartment.Id)
                return BadRequest(ModelState);

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var departmentMap = _mapper.Map<Department>(updatedDepartment);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartment(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
            {
                return NotFound();
            }

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting category");
            }

            return NoContent();
        }
    }
}