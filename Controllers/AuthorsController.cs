using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using CoursesLibrary.Helpers;
using AutoMapper;
using CoursesLibrary.Entities;
using CoursesLibrary.Models;
using CoursesLibrary.Services;

namespace CoursesLibrary.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthorsController : ControllerBase
  {
    private readonly ICoursesLibraryRepository _CoursesLibraryRepository;
    private readonly IMapper _mapper;
    public AuthorsController(ICoursesLibraryRepository CoursesLibraryRepository, IMapper mapper)
    {
      _CoursesLibraryRepository = CoursesLibraryRepository;

      _mapper = mapper;
    }
    [HttpGet()]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
    {
      var authorsFromRepo = _CoursesLibraryRepository.GetAuthors();

      var authors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);

      return new OkObjectResult(authors);
    }

    [HttpGet("{authorId:guid}")]
    public ActionResult<Author> GetAuthor(Guid authorId)
    {
      var authorFromRepo = _CoursesLibraryRepository.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return new OkObjectResult(_mapper.Map<AuthorDto>(authorFromRepo));
    }
  }
}
