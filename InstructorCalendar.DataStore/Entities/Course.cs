using InstructorCalendar.DataStore.CommandModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructorCalendar.DataStore.Entities
{
    internal class Course : CourseData
    {
        [Key]
        public Guid Id { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
        }
    }
}
