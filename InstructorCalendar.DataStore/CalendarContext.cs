using InstructorCalendar.DataStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructorCalendar.DataStore
{
    internal class CalendarContext : DbContext
    {
        public CalendarContext() : base("DefaultConnection") { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<ScheduledRoom> ScheduledRooms { get; set; }
    }
}
