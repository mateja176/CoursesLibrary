using CourseLibrary.API.Entities;
using DotnetWebAPIDemo.Models;
using DotnetWebAPIDemo.Helpers;

namespace DotnetWebAPIDemo.Profile
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
