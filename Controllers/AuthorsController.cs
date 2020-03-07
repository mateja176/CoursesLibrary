using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetWebAPIDemo.Controllers
{
  [ApiController]
  public class AuthorsController : ControllerBase
  {
    private readonly ICourseLibraryRepository _courseLibraryRepository;
    public AuthorsController(ICourseLibraryRepository courseLibraryRepository)
    {
      _courseLibraryRepository = courseLibraryRepository;
    }
    [HttpGet("api/authors")]
    public IActionResult GetAuthors()
    {
      var authorsFromRepo = _courseLibraryRepository.GetAuthors();

      return new JsonResult(authorsFromRepo);
    }
  }
}
