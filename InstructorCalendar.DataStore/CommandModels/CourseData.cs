using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructorCalendar.DataStore.CommandModels
{
    public class CourseData
    {
        public string Subject { get; set; }
        public string Catalog { get; set; }
        public string ClassTitle { get; set; }

        public List<CourseOfferingData> Offerings { get; set; }

        public CourseData()
        {
            Offerings = new List<CourseOfferingData>();
        }
    }
}
