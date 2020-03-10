using System.ComponentModel.DataAnnotations;
using CoursesLibrary.Models;

namespace CoursesLibrary.ValidationAttributes
{
    public class CourseTitleMustBeDifferentFromDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseForCreationDto) validationContext.ObjectInstance;

            if (course.Title == course.Description)
            {
                return new ValidationResult(
                    "The provided description should be different from the title",
                    new[] {nameof(CourseForCreationDto)});
            }

            return ValidationResult.Success;
        }
    }
}