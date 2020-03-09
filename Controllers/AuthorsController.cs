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

            return Ok(authors);
        }

        [HttpGet("{authorId:guid}", Name = "GetAuthor")]
        public ActionResult<Author> GetAuthor(Guid authorId)
        {
            var authorFromRepo = _coursesLibraryRepository.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AuthorDto>(authorFromRepo));
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreation author)
        {
            var authorEntity = _mapper.Map<Author>(author);

            _coursesLibraryRepository.AddAuthor(authorEntity);

            _coursesLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);

            return CreatedAtRoute("GetAuthor", new {authorId = authorToReturn.Id}, authorToReturn);
        }
    }
}