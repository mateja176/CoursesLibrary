using System;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

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

      return new OkObjectResult(authorsFromRepo);
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
