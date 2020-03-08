using System;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using DotnetWebAPIDemo.Models;
using DotnetWebAPIDemo.Helpers;
using CourseLibrary.API.Entities;
using AutoMapper;

namespace DotnetWebAPIDemo.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    private readonly IMapper _mapper;
    public AuthorsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
    {
      _courseLibraryRepository = courseLibraryRepository;

      _mapper = mapper;
    }
    [HttpGet()]
    public ActionResult<IEnumerable<AuthorDto>> GetAuthors()
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors();

      var authors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);

      return new OkObjectResult(authors);
    }

    [HttpGet("{authorId:guid}")]
    public ActionResult<Author> GetAuthor(Guid authorId)
    {
      var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return new OkObjectResult(_mapper.Map<AuthorDto>(authorFromRepo));
    }
  }
}
