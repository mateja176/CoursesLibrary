using CourseLibrary.API.Entities;
using CoursesLibrary.Models;
using CoursesLibrary.Helpers;

namespace CoursesLibrary.Profile
{
  public class AuthorsProfile : AutoMapper.Profile
  {
    public AuthorsProfile()
    {
      CreateMap<Author, AuthorDto>()
          .ForMember(dest => dest.Name,
              opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
          .ForMember(
              dest => dest.Age,
              opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge()));
    }
  }
}
