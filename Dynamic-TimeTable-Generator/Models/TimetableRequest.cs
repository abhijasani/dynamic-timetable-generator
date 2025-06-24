using System.ComponentModel.DataAnnotations;

namespace TimetableGenerator.Models
{
    /// <summary>
    /// Represents a request for a timetable configuration with validation.
    /// </summary>
    public class TimetableRequest : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the timetable configuration.
        /// </summary>
        public TimetableConfiguration Configuration { get; set; } = new();
        /// <summary>
        /// Gets or sets the list of subjects for the timetable request.
        /// </summary>
        public List<Subject> Subjects { get; set; } = new();

        /// <summary>
        /// Validates the timetable request.
        /// </summary>
        /// <param name="validationContext">The context for validation.</param>
        /// <returns>A collection of validation results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var totalSubjectHours = Subjects.Sum(s => s.Hours);
            if (totalSubjectHours != Configuration.TotalHoursPerWeek)
            {
                yield return new ValidationResult(
                    $"Total subject hours ({totalSubjectHours}) must equal total hours per week ({Configuration.TotalHoursPerWeek})",
                    new[] { nameof(Subjects) });
            }

            if (Subjects.Count != Configuration.TotalSubjects)
            {
                yield return new ValidationResult(
                    $"Number of subjects ({Subjects.Count}) must match total subjects ({Configuration.TotalSubjects})",
                    new[] { nameof(Subjects) });
            }
        }
    }
}