using System;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using DotnetWebAPIDemo.Models;
using DotnetWebAPIDemo.Helpers;

namespace DotnetWebAPIDemo.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
    {
      _courseLibraryRepository = courseLibraryRepository;
    }
    [HttpGet()]
    public IActionResult GetAuthors()
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors();

      var authors = authorsFromRepo.Select(authorFromRepo => new AuthorDto()
      {
        Id = authorFromRepo.Id,
        Name = $"{authorFromRepo.FirstName} {authorFromRepo.LastName}",
        MainCategory = authorFromRepo.MainCategory,
        Age = authorFromRepo.DateOfBirth.GetCurrentAge()
      });

      return new OkObjectResult(authors);
    }

    [HttpGet("{authorId:guid}")]
    public IActionResult GetAuthor(Guid authorId)
    {
      var authorFromRepo = _courseLibraryRepository.GetAuthor(authorId);

      if (authorFromRepo == null)
      {
        return NotFound();
      }

      return new OkObjectResult(authorFromRepo);
    }
  }
}
