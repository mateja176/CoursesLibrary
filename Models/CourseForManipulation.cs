using System.ComponentModel.DataAnnotations;
using CoursesLibrary.ValidationAttributes;

namespace CoursesLibrary.Models
{
    [CourseTitleMustBeDifferentFromDescription]
    public abstract class CourseForManipulation
    {
        [Required(ErrorMessage = "A course title is required.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)] public virtual string Description { get; set; }
    }
}