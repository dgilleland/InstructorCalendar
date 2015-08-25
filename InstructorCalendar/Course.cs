using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstructorCalendar
{
    public class Course
    {
        public string Subject { get; set; }
        public string Catalog { get; set; }
        public string ClassTitle { get; set; }

        public List<CourseOffering> Offerings { get; set; }

        public Course()
        {
            Offerings = new List<CourseOffering>();
        }
    }
    public class CourseOffering
    {
        public string Section { get; set; }
        // TODO: Start and end dates

        public List<ScheduledRoom> Rooms { get; set; }

        public CourseOffering()
        {
            Rooms = new List<ScheduledRoom>();
        }
    }
    public class ScheduledRoom
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek MeetingDay { get; set; }
        public string Room { get; set; }
    }
    public class ScheduledCourse
    {
        public DayOfWeek MeetingDay { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string RoomNumber { get; set; }
        public string CourseNumber { get; set; }
        public string CourseTitle { get; set; }
        public string Section { get; set; }
    }
}