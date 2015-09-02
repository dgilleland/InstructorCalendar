using System;
using System.Collections.Generic;
using System.Linq;

namespace InstructorCalendar.DataStore.CommandModels
{
    public static class CourseColors
    {
        public static IList<string> Colors()
        {
            List<string> colors = new List<string>();
            colors.Add("#33FF00"); // Green-ish
            colors.Add("#33FFFF"); // Blue-ish
            colors.Add("#FFFF33"); // Yellow-ish
            colors.Add("#FF99CC"); // Pink-ish
            colors.Add("#FF9900"); // Orange-ish
            colors.Add("#C0C0C0"); // Gray-ish
            colors.Add("#339966"); // Woodsy-Green-ish
            return colors;
        }
    }
}
