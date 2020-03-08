using System;
using System.Collections.Generic;
using CoursesLibrary.Entities;

namespace CoursesLibrary.Services
{
  public interface ICoursesLibraryRepository
  {
    IEnumerable<Course> GetCourses(Guid authorId);
    Course GetCourse(Guid authorId, Guid courseId);
    void AddCourse(Guid authorId, Course course);
    void UpdateCourse(Course course);
    void DeleteCourse(Course course);
    IEnumerable<Author> GetAuthors();
    Author GetAuthor(Guid authorId);
    IEnumerable<Author> GetAuthors(string mainCategory);
    IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
    void AddAuthor(Author author);
    void DeleteAuthor(Author author);
    void UpdateAuthor(Author author);
    bool AuthorExists(Guid authorId);
    bool Save();
  }
}
