using System;

namespace Storing_Dates_and_Times_in_SQL_Server
{
    public class Session
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public TimeSpan Length { get; set; }
        public DateTimeOffset ScheduledAt { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
    }
}
