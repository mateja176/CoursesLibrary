using System;

namespace CoursesLibrary.Helpers
{
  public static class DateTimeOffsetExtensions
  {
    public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
    {
      var currentDate = DateTime.UtcNow;
      int age = currentDate.Year - dateTimeOffset.Year;

      return currentDate < dateTimeOffset.AddYears(age) ? age - 1 : age;
    }
  }
}
