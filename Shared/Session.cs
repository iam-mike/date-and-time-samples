using System;

namespace Shared
{
    public class Session
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public DateTimeOffset ScheduledAt { get; set; }
        public DateTimeOffset SubmittedAt { get; set; }
    }
}
