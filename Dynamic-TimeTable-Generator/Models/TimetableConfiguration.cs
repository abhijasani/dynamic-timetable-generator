using System.ComponentModel.DataAnnotations;

namespace TimetableGenerator.Models
{
    /// <summary>
    /// Represents the configuration for a timetable, including working days, subjects per day, and total subjects.
    /// </summary>
    public class TimetableConfiguration
    {
        /// <summary>
        /// Gets or sets the number of working days in the timetable.
        /// </summary>
        [Range(1, 7, ErrorMessage = "Working days must be between 1 and 7")]
        public int WorkingDays { get; set; }

        /// <summary>
        /// Gets or sets the number of subjects scheduled per day.
        /// </summary>
        [Range(1, 8, ErrorMessage = "Subjects per day must be less than 9 And greater than 0")]
        public int SubjectsPerDay { get; set; }

        /// <summary>
        /// Gets or sets the total number of subjects in the timetable.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Total subjects must be a positive number")]
        public int TotalSubjects { get; set; }

        /// <summary>
        /// Gets the total hours per week based on working days and subjects per day.
        /// </summary>
        public int TotalHoursPerWeek => WorkingDays * SubjectsPerDay;
    }
}