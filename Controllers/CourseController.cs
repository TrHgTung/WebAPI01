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
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IReviewRepository reviewRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Course>))]
        public IActionResult GetCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetCourses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else return Ok(courses);
        }
        [HttpGet("{courseId}")]
        [ProducesResponseType(200, Type = typeof(Course))]
        [ProducesResponseType(400)]
        public IActionResult GetCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var course = _mapper.Map<CourseDto>(_courseRepository.GetCourse(courseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(course);

        }

        [HttpGet("{courseId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetCourseRating(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            var rating = _courseRepository.GetCourseRating(courseId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCourse([FromQuery] int teacherId, [FromQuery] int catId, [FromBody] CourseDto courseCreate)
        {
            if (courseCreate == null)
                return BadRequest(ModelState);

            //var pokemons = _pokemonRepository.GetPokemons().Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
            //    .FirstOrDefault();
            var courses = _courseRepository.GetCourseTrimToUpper(courseCreate);

            if (courses != null)
            {
                ModelState.AddModelError("", "This course already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var courseMap = _mapper.Map<Course>(courseCreate);

            if (!_courseRepository.CreateCourse(teacherId, catId, courseMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving..");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created");
        }


        [HttpPut("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCourse(int courseId,
            [FromQuery] int teacherId, [FromQuery] int catId,
            [FromBody] CourseDto updatedCourse)
        {
            if (updatedCourse == null)
                return BadRequest(ModelState);

            if (courseId != updatedCourse.Id)
                return BadRequest(ModelState);

            if (!_courseRepository.CourseExists(courseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var courseMap = _mapper.Map<Course>(updatedCourse);

            if (!_courseRepository.UpdateCourse(teacherId, catId, courseMap))
            {
                ModelState.AddModelError("", "Something went wrong updating teacher");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{courseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCourse(int courseId)
        {
            if (!_courseRepository.CourseExists(courseId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfACourse(courseId);
            var courseToDelete = _courseRepository.GetCourse(courseId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_courseRepository.DeleteCourse(courseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting teacher");
            }

            return NoContent();
        }
    }
}