using InstructorCalendar.DataStore.CommandModels;
using InstructorCalendar.DataStore.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstructorCalendar.DataStore
{
    public class CalendarRepository
    {
        public static CalendarRepository Create()
        {
            return new CalendarRepository();
        }
        #region Commands
        public List<CourseData> ParseInstructorSchedule(string schedule)
        {
            List<CourseData> courses = new List<CourseData>();

            string[] lines = { };
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
                int color = 0;
                Dictionary<string, string> lineDetails;
                for (int row = 1; row < lines.Length; row++)
                {
                    lineDetails = new Dictionary<string, string>();
                    string[] details = lines[row].Split(tabDelimiter);
                    for (int col = 0; col < headings.Length; col++)
                    {
                        lineDetails.Add(headings[col], details[col]);
                    }
                    CourseData course = new CourseData()
                    {
                        ClassTitle = lineDetails["ClassTitle"],
                        Catalog = lineDetails["Catalog"],
                        Subject = lineDetails["Subject"]
                    };
                    course.Offerings.Add(new CourseOfferingData() { Section = lineDetails["Section"] });
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
                    if (days.Contains("Mo"))
                    {
                        meetingDays.Add(DayOfWeek.Monday);
                    }
                    if (days.Contains("Tu"))
                    {
                        meetingDays.Add(DayOfWeek.Tuesday);
                    }
                    if (days.Contains("We"))
                    {
                        meetingDays.Add(DayOfWeek.Wednesday);
                    }
                    if (days.Contains("Th"))
                    {
                        meetingDays.Add(DayOfWeek.Thursday);
                    }
                    if (days.Contains("Fr"))
                    {
                        meetingDays.Add(DayOfWeek.Friday);
                    }
                    if (days.Contains("Sa"))
                    {
                        meetingDays.Add(DayOfWeek.Saturday);
                    }
                    if (days.Contains("Su"))
                    {
                        meetingDays.Add(DayOfWeek.Sunday);
                    }
                    foreach (var day in meetingDays)
                    {
                        course.Offerings[0].Rooms.Add(new ScheduledRoomData() { StartTime = startTime, EndTime = endTime, MeetingDay = day, Room = lineDetails["Room"] });
                    }

                    if (courses.Any(x => x.Catalog == course.Catalog && x.Subject == course.Subject))
                    {
                        var existing = courses.First(x => x.Catalog == course.Catalog && x.Subject == course.Subject);
                        if (existing.Offerings.Any(x => x.Section == course.Offerings[0].Section))
                        {
                            var offering = existing.Offerings.First(x => x.Section == course.Offerings[0].Section);
                            foreach (var item in course.Offerings[0].Rooms)
                            {
                                offering.Rooms.Add(item);
                            }
                        }
                        else
                        {
                            course.Offerings[0].BackgroundColor = CourseColors.Colors()[color];
                            color++;
                            existing.Offerings.Add(course.Offerings[0]);
                        }
                    }
                    else
                    {
                        course.Offerings[0].BackgroundColor = CourseColors.Colors()[color];
                        color++;
                        courses.Add(course);
                    }
                }
            }

            return courses;
        }
        public void SaveOrUpdate(List<CourseData> courses, string userId)
        {
            using (var context = new CalendarContext())
            {
                
            }
        }
        #endregion

        #region Queries
        public List<ScheduledCourse> GenerateCalendarGraph(List<CourseData> courses)
        {
            List<ScheduledCourse> output = new List<ScheduledCourse>();
            // Consolodate the list of courses into a single Calendar graph
            var weekSchedule = from course in courses
                               from offering in course.Offerings
                               from room in offering.Rooms
                               select new ScheduledCourse()
                               {
                                   MeetingDay = room.MeetingDay,
                                   Start = room.StartTime,
                                   End = room.EndTime,
                                   RoomNumber = room.Room,
                                   CourseNumber = course.Subject + course.Catalog,
                                   CourseTitle = course.ClassTitle,
                                   Section = offering.Section,
                                   BackgroundColor = offering.BackgroundColor
                               };
            weekSchedule = weekSchedule.OrderBy(s => s.MeetingDay);
            output = weekSchedule.ToList();
            return output;
        }
        #endregion
    }
}
