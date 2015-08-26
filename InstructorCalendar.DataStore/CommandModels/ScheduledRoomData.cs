using System;
using System.Collections.Generic;
using System.Linq;

namespace InstructorCalendar.DataStore.CommandModels
{
    public class ScheduledRoomData
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek MeetingDay { get; set; }
        public string Room { get; set; }
    }
}
