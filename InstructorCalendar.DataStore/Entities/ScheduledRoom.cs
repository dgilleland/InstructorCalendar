using InstructorCalendar.DataStore.CommandModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InstructorCalendar.DataStore.Entities
{
    internal class ScheduledRoom : ScheduledRoomData
    {
        [Key]
        public Guid Id { get; set; }

        public virtual CourseOffering CourseOffering { get; set; }

        public ScheduledRoom()
        {
            Id = Guid.NewGuid();
        }
    }
}
