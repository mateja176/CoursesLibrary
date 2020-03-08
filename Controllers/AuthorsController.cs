using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using CoursesLibrary.Helpers;
using AutoMapper;
using CoursesLibrary.DbContexts;
using CoursesLibrary.Entities;
using CoursesLibrary.Models;
using CoursesLibrary.ResourceParameters;
using CoursesLibrary.Services;

namespace CoursesLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICoursesLibraryRepository _coursesLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsController(ICoursesLibraryRepository coursesLibraryRepository, IMapper mapper)
        {
            _coursesLibraryRepository = coursesLibraryRepository;

            _mapper = mapper;
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery] AuthorsResourceParameters parameters)
        {
            var authorsFromRepo = _coursesLibraryRepository.GetAuthors(parameters);

            var authors = _mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo);

            return new OkObjectResult(authors);
        }

        [HttpGet("{authorId:guid}")]
        public ActionResult<Author> GetAuthor(Guid authorId)
        {
            var authorFromRepo = _coursesLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return new OkObjectResult(_mapper.Map<AuthorDto>(authorFromRepo));
        }
    }
}