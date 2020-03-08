using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using CoursesLibrary.Models;
using CoursesLibrary.Services;
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

    [HttpGet("{courseId}")]
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
  }
}
