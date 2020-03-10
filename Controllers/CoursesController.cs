using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CoursesLibrary.Entities;
using CoursesLibrary.Models;
using CoursesLibrary.Services;
using CoursesLibrary.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;

namespace CoursesLibrary.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesLibraryRepository _coursesLibraryRepository;
        private readonly IMapper _mapper;

        public CoursesController(ICoursesLibraryRepository coursesLibraryRepository, IMapper mapper)
        {
            _coursesLibraryRepository = coursesLibraryRepository;
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_coursesLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthor = _coursesLibraryRepository.GetCourses(authorId);

            var courses = _mapper.Map<IEnumerable<CoursesDto>>(coursesForAuthor);

            return Ok(courses);
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CoursesDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_coursesLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var coursesForAuthorFromRepo = _coursesLibraryRepository.GetCourse(authorId, courseId);

            if (coursesForAuthorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CoursesDto>(coursesForAuthorFromRepo));
        }

        [HttpPost]
        public ActionResult<CoursesDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto course)
        {
            if (!_coursesLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);

            _coursesLibraryRepository.AddCourse(authorId, courseEntity);

            _coursesLibraryRepository.Save();

            var courseDto = _mapper.Map<CoursesDto>(courseEntity);

            return CreatedAtRoute("GetCourseForAuthor", new {authorId = authorId, courseId = courseDto.Id}, courseDto);
        }

        [HttpPut("{courseId}")]
        public IActionResult SetCourseForAuthor(Guid authorId, Guid courseId, CourseForSettingDto courseDto)
        {
            if (!_coursesLibraryRepository.AuthorExists((authorId)))
            {
                return NotFound();
            }

            var courseFromRepo = _coursesLibraryRepository.GetCourse(authorId, courseId);

            if (courseFromRepo == null)
            {
                var courseToAdd = _mapper.Map<Course>(courseDto);

                courseToAdd.Id = courseId;

                _coursesLibraryRepository.AddCourse(authorId, courseToAdd);

                _coursesLibraryRepository.Save();

                var courseToReturn = _mapper.Map<CoursesDto>(courseToAdd);

                return CreatedAtRoute("GetCourseForAuthor", new {authorId, courseId},
                    courseToReturn);
            }

            _coursesLibraryRepository.UpdateCourse(_mapper.Map(courseDto, courseFromRepo));

            _coursesLibraryRepository.Save();

            return NoContent();
        }
    }
}