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
    private readonly ICoursesLibraryRepository _CoursesLibraryRepository;
    private readonly IMapper _mapper;

    public CoursesController(ICoursesLibraryRepository CoursesLibraryRepository, IMapper mapper)
    {
      _CoursesLibraryRepository = CoursesLibraryRepository;
      _mapper = mapper;
    }

    public ActionResult<IEnumerable<CoursesDto>> GetCoursesForAuthor(Guid authorId)
    {
      if (!_CoursesLibraryRepository.AuthorExists(authorId))
      {
        return NotFound();
      }

      var coursesForAuthor = _CoursesLibraryRepository.GetCourses(authorId);

      var courses = _mapper.Map<IEnumerable<CoursesDto>>(coursesForAuthor);

      return Ok(courses);
    }

    [HttpGet("{courseId}")]
    public ActionResult<CoursesDto> GetCourseForAuthor(Guid authorId, Guid courseId)
    {
      if (!_CoursesLibraryRepository.AuthorExists(authorId))
      {
        return NotFound();
      }

      var coursesForAuthorFromRepo = _CoursesLibraryRepository.GetCourse(authorId, courseId);

      if (coursesForAuthorFromRepo == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<CoursesDto>(coursesForAuthorFromRepo));
    }
  }
}
