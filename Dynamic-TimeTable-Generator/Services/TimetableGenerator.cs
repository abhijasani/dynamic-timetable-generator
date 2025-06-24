using TimetableGenerator.Interfaces;
using TimetableGenerator.Models;

namespace TimetableGenerator.Services
{
    /// <summary>
    /// Service for generating timetables based on configuration and subjects.
    /// </summary>
    public class TimetableGeneratorService : ITimetableGenerator
    {
        /// <summary>
        /// Generates a timetable based on the provided configuration and subjects.
        /// </summary>
        public string[,] GenerateTimetable(TimetableConfiguration configuration, List<Subject> subjects)
        {
            var timetable = new string[configuration.SubjectsPerDay, configuration.WorkingDays];
            var subjectPool = CreateSubjectPool(subjects);
            
            // Shuffle for random distribution
            var random = new Random();
            subjectPool = subjectPool.OrderBy(x => random.Next()).ToList();

            int index = 0;
            for (int row = 0; row < configuration.SubjectsPerDay; row++)
            {
                for (int col = 0; col < configuration.WorkingDays; col++)
                {
                    if (index < subjectPool.Count)
                    {
                        timetable[row, col] = subjectPool[index];
                        index++;
                    }
                }
            }

            return timetable;
        }

        private List<string> CreateSubjectPool(List<Subject> subjects)
        {
            var pool = new List<string>();
            foreach (var subject in subjects)
            {
                for (int i = 0; i < subject.Hours; i++)
                {
                    pool.Add(subject.Name);
                }
            }
            return pool;
        }
    }
}