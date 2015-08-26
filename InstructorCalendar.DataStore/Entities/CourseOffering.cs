using InstructorCalendar.DataStore.CommandModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace InstructorCalendar.DataStore.Entities
{
    internal class CourseOffering : CourseOfferingData
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Course Course { get; set; }

        public CourseOffering()
        {
            Id = Guid.NewGuid();
        }
    }
}
