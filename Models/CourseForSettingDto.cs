using System.ComponentModel.DataAnnotations;

namespace CoursesLibrary.Models
{
    public class CourseForSettingDto : CourseForManipulation
    {
        [Required]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}