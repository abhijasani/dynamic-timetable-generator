using System.ComponentModel.DataAnnotations;

namespace TimetableGenerator.Models
{
    /// <summary>
    /// Represents a subject in the timetable.
    /// </summary>
    public class Subject
    {
        
        /// <summary>
        /// Gets or sets the name of the subject.
        /// </summary>
        [Required(ErrorMessage = "Subject name is required")]
        [StringLength(50, ErrorMessage = "Subject name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of hours for the subject.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Hours must be a positive number")]
        public int Hours { get; set; }
    }
}