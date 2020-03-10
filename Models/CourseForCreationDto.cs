using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CoursesLibrary.ValidationAttributes;

namespace CoursesLibrary.Models
{
    public class CourseForCreationDto : CourseForManipulation
    {
        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //     if (Title == Description)
        //     {
        //         yield return new ValidationResult(
        //             "The provided description should be different from the title.", new[] {"CourseForCreationDto"});
        //     }
        // }
    }
}