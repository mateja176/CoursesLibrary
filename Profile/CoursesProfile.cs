using CoursesLibrary.Entities;
using CoursesLibrary.Models;

namespace CoursesLibrary.Profile
{
    public class CoursesProfile : AutoMapper.Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CoursesDto>();
            
            CreateMap<CourseForCreationDto, Course>();
        }
    }
}