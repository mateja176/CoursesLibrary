using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoursesLibrary.Entities;
using CoursesLibrary.Helpers;
using CoursesLibrary.Models;
using CoursesLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesLibrary.Controllers
{
    [ApiController]
    [Route("api/authors_collection")]
    public class AuthorsCollectionController : ControllerBase
    {
        private readonly ICoursesLibraryRepository _coursesLibraryRepository;
        private readonly IMapper _mapper;

        public AuthorsCollectionController(ICoursesLibraryRepository coursesLibraryRepository, IMapper mapper)
        {
            _coursesLibraryRepository = coursesLibraryRepository;

            _mapper = mapper;
        }

        [HttpGet("({ids})", Name = "GetAuthorsCollection")]
        public ActionResult<IEnumerable<Author>> GetAuthorsCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var authors = _coursesLibraryRepository.GetAuthors(ids);

            if (ids.Count() != authors.Count())
            {
                return NotFound();
            }

            return Ok(authors);
        }

        [HttpPost]
        public ActionResult<IEnumerable<Author>> CreateAuthors(IEnumerable<AuthorForCreation> authorsForCreation)
        {
            var authors = _mapper.Map<IEnumerable<Author>>(authorsForCreation);

            foreach (var author in authors)
            {
                _coursesLibraryRepository.AddAuthor(author);
            }

            _coursesLibraryRepository.Save();

            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);

            var idsAsString = string.Join(",", authorDtos.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorsCollection", new {ids = idsAsString}, authorDtos);
        }
    }
}