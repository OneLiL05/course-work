namespace trade_compas.Helpers;

public class TimeHelper
{
    public static string GetRelativeTime(DateTime time)
    {
        var timeDifference = DateTime.Now - time;

        if (timeDifference.TotalSeconds < 60)
        {
            return "just now";
        }

        if (timeDifference.TotalMinutes < 60)
        {
            var minutes = (int)timeDifference.TotalMinutes;
            return minutes == 1 ? "a minute ago" : $"{minutes} minutes ago";
        }

        if (timeDifference.TotalHours < 24)
        {
            var hours = (int)timeDifference.TotalHours;
            return hours == 1 ? "an hour ago" : $"{hours} hours ago";
        }

        if (timeDifference.TotalDays < 7)
        {
            var days = (int)timeDifference.TotalDays;
            return days == 1 ? "a day ago" : $"{days} days ago";
        }

        if (timeDifference.TotalDays < 30)
        {
            var weeks = (int)(timeDifference.TotalDays / 7);
            return weeks == 1 ? "a week ago" : $"{weeks} weeks ago";
        }
        if (timeDifference.TotalDays < 365)
        {
            var months = (int)(timeDifference.TotalDays / 30);
            return months == 1 ? "a month ago" : $"{months} months ago";
        }

        var years = (int)(timeDifference.TotalDays / 365);
        return years == 1 ? "a year ago" : $"{years} years ago";
    }
}