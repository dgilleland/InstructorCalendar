using System;
using System.Collections.Generic;
using System.Linq;

namespace InstructorCalendar.DataStore.CommandModels
{
    public class CourseOfferingData
    {
        public string Section { get; set; }
        // TODO: Start and end dates

        public List<ScheduledRoomData> Rooms { get; set; }

        public CourseOfferingData()
        {
            Rooms = new List<ScheduledRoomData>();
        }
    }
}
