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
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Teacher>))]
        public IActionResult GetTeachers()
        {
            var teachers = _mapper.Map<List<TeacherDto>>(_teacherRepository.GetTeachers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(teachers);
        }

        [HttpGet("{teacherId}")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            var teacher = _mapper.Map<TeacherDto>(_teacherRepository.GetTeacher(teacherId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(teacher);

        }
        [HttpGet("{teacherId}/course")]
        [ProducesResponseType(200, Type = typeof(Teacher))]
        [ProducesResponseType(400)]
        public IActionResult GetCourseByTeacher(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }
            var teacher = _mapper.Map<List<CourseDto>>(_teacherRepository.GetCourseByTeacher(teacherId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
                return Ok(teacher);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTeacher([FromQuery] int departmentId, [FromBody] TeacherDto teacherCreate)
        {
            if (teacherCreate == null)
                return BadRequest(ModelState);

            var teachers = _teacherRepository.GetTeachers().Where(c => c.LastName.Trim().ToUpper() == teacherCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (teachers != null)
            {
                ModelState.AddModelError("", "This teacher already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var teacherMap = _mapper.Map<Teacher>(teacherCreate);

            teacherMap.Department = _departmentRepository.GetDepartment(departmentId);

            if (!_teacherRepository.CreateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving..");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }

        [HttpPut("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTeacher(int teacherId, [FromBody] TeacherDto updatedTeacher)
        {
            if (updatedTeacher == null)
                return BadRequest(ModelState);

            if (teacherId != updatedTeacher.Id)
                return BadRequest(ModelState);

            if (!_teacherRepository.TeacherExists(teacherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var teacherMap = _mapper.Map<Teacher>(updatedTeacher);

            if (!_teacherRepository.UpdateTeacher(teacherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating teacher");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{teacherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOwner(int teacherId)
        {
            if (!_teacherRepository.TeacherExists(teacherId))
            {
                return NotFound();
            }

            var ownerToDelete = _teacherRepository.GetTeacher(teacherId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_teacherRepository.DeleteTeacher(ownerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting teacher");
            }

            return NoContent();
        }
    }
}