using CourseLibrary.API.Entities;
using DotnetWebAPIDemo.Models;

namespace DotnetWebAPIDemo.Profile
{
    public class CoursesProfile : AutoMapper.Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CoursesDto>();
        }
    }
}