using System;
using System.Collections.Generic;
using System.Linq;

namespace InstructorCalendar.DataStore.QueryModels
{
    public class ScheduledCourse
    {
        public DayOfWeek MeetingDay { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string RoomNumber { get; set; }
        public string CourseNumber { get; set; }
        public string CourseTitle { get; set; }
        public string Section { get; set; }
        public string BackgroundColor { get; set; }
    }
}
