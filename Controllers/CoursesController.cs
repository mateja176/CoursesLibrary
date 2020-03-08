using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CourseLibrary.API.Services;
using CoursesLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoursesLibrary.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
        }

        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthor = _courseLibraryRepository.GetCourses(authorId);

            var courses = _mapper.Map<IEnumerable<CoursesDto>>(coursesForAuthor);

            return Ok(courses);
        }

        [HttpGet("{courseId}")]
        public ActionResult<CoursesDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (coursesForAuthorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CoursesDto>(coursesForAuthorFromRepo));
        }
    }
}