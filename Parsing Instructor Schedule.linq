<Query Kind="Program" />

void Main()
{
        string schedule = "\r\nSubject\tCatalog\tSection\tComponent\tClass Title\tStart Time\tEnd Time\tMeeting Days\tStart Date\tEnd Date\tRoom\r\nDMIT\t1508\tA04\tLLB\tDatabase Fundamentals\t1:15 PM\t3:15 PM\tTu\t9/8/2015\t12/18/2015\tWA302\r\nDMIT\t1508\tA04\tLLB\tDatabase Fundamentals\t1:15 PM\t3:15 PM\tFr\t9/8/2015\t12/18/2015\tWA322\r\nDMIT\t1508\tA04\tLLB\tDatabase Fundamentals\t1:15 PM\t3:15 PM\tTh\t9/8/2015\t12/18/2015\tWB304\r\nDMIT\t1508\tA05\tLLB\tDatabase Fundamentals\t11:15 AM\t1:15 PM\tWe\t9/8/2015\t12/18/2015\tWA322\r\nDMIT\t1508\tA05\tLLB\tDatabase Fundamentals\t10:15 AM\t12:15 PM\tTu\t9/8/2015\t12/18/2015\tWB308\r\nDMIT\t1508\tA05\tLLB\tDatabase Fundamentals\t10:15 AM\t12:15 PM\tMo\t9/8/2015\t12/18/2015\tWC306\r\nDMIT\t2018\tA02\tLLB\tIntermediate Application Dev.\t3:15 PM\t5:15 PM\tMoTuTh\t9/8/2015\t12/18/2015\tWA320";
        string[] lines = {};
        List<Course> courses = new List<Course>();
        var IsPost = true;
        if (IsPost)
        {
            //schedule = Request["schedule"];
            string[] lineDelimiters = { Environment.NewLine };
            char tabDelimiter = '\t';
            lines = schedule.Split(lineDelimiters, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length > 0)
            {
                // Turn headings into keys for line details
                string[] headings = lines[0].Split(tabDelimiter);
                for (int index = 0; index < headings.Length; index++)
                {
                    headings[index] = headings[index].Replace(" ", "");
                }
                
                // Create a Course object graph for each line
                Dictionary<string, string> lineDetails;
                for (int row = 1; row < lines.Length; row++)
                {
                    lineDetails = new Dictionary<string, string>();
                    string[] details = lines[row].Split(tabDelimiter);
                    for (int col = 0; col < headings.Length; col++)
                    {
                        lineDetails.Add(headings[col], details[col]);
                    }
                    Course course = new Course()
                    {
                        ClassTitle = lineDetails["ClassTitle"],
                        Catalog = lineDetails["Catalog"],
                        Subject = lineDetails["Subject"]
                    };
                    course.Offerings.Add(new CourseOffering() { Section = lineDetails["Section"] });
                    string time;
                    time = lineDetails["StartTime"].ToLower();
                    if (time.Length == 7)
                    {
                        time = "0" + time;
                    }
                    var startTime = DateTime.ParseExact(time, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
                    time = lineDetails["EndTime"].ToLower();
                    if (time.Length == 7)
                    {
                        time = "0" + time;
                    }
                    var endTime = DateTime.ParseExact(time, "hh:mm tt", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
                    List<DayOfWeek> meetingDays = new List<DayOfWeek>();
                    string days = lineDetails["MeetingDays"];
                    if(days.Contains("Mo"))
                    {
                        meetingDays.Add(DayOfWeek.Monday);
                    }
                    if(days.Contains("Tu"))
                    {
                        meetingDays.Add(DayOfWeek.Tuesday);
                    }
                    if(days.Contains("We"))
                    {
                        meetingDays.Add(DayOfWeek.Wednesday);
                    }
                    if(days.Contains("Th"))
                    {
                        meetingDays.Add(DayOfWeek.Thursday);
                    }
                    if(days.Contains("Fr"))
                    {
                        meetingDays.Add(DayOfWeek.Friday);
                    }
                    if(days.Contains("Sa"))
                    {
                        meetingDays.Add(DayOfWeek.Saturday);
                    }
                    if(days.Contains("Su"))
                    {
                        meetingDays.Add(DayOfWeek.Sunday);
                    }
                    foreach (var day in meetingDays)
                    {
                        course.Offerings[0].Rooms.Add(new ScheduledRoom() { StartTime = startTime, EndTime = endTime, MeetingDay = day, Room = lineDetails["Room"] });
                    }
    
                    courses.Add(course);
                }
                
                //courses.Dump();
                // Consolodate the list of courses into a single Calendar graph
                var weekSchedule = from course in courses
                                   from offering in course.Offerings
                                   from room in offering.Rooms
                                   select new
                                    {
                                        MeetingDay = room.MeetingDay,
                                        Start = room.StartTime,
                                        End = room.EndTime,
                                        RoomNumber = room.Room,
                                        CourseNumber = course.Subject + course.Catalog,
                                        CourseTitle = course.ClassTitle,
                                        Section = offering.Section
                                    };
                                    weekSchedule = weekSchedule.OrderBy (s => s.MeetingDay);
                weekSchedule.Dump();
            }
        }
    
}

// Define other methods and classes here
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
