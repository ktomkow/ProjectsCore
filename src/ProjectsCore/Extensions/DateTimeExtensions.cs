using System;

namespace ProjectsCore.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly TimeSpan span = TimeSpan.FromMilliseconds(1);

        public static bool IsEqualTo(this DateTime left, DateTime right)
        {
            TimeSpan difference = left - right;
            return difference <= span;            
        }
    }
}
