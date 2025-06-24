namespace TimetableGenerator.Models
{
    /// <summary>
    /// Represents the response containing the timetable data.
    /// </summary>
    public class TimetableResponse
    {
        public string[,] Timetable { get; set; } = new string[0, 0];
        public int Rows { get; set; }
        public int Columns { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Converts the timetable from a 2D array to a jagged array.
        /// </summary>
        /// <returns>A jagged array representation of the timetable.</returns>
        public string[][] GetTimetableAsJaggedArray()
        {
            var result = new string[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                result[i] = new string[Columns];
                for (int j = 0; j < Columns; j++)
                {
                    result[i][j] = Timetable[i, j];
                }
            }
            return result;
        }
    }
}