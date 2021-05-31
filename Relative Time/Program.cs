using Shared;
using System;
using System.Linq;
using static System.Console;

namespace Relative_Time
{
    class Program
    {
        static void Main(string[] args)
        {
            var speaker = GetSpeaker();

            WriteLine($"Sessions for: {speaker.Name}");
            WriteLine("-----------------------------");

            foreach (var session in speaker.Sessions)
            {
                WriteLine($"{session.Title}");
                WriteLine($"Starts: {session.ScheduledAt}");
                WriteLine($"Ends:   {session.ScheduledAt.Add(session.Length)}");

                var doesSpeakerHaveEarlierSessions = DoesSpeakerHaveEalierSessions(speaker, session);
                var doesSpeakerHaveLaterSessions = DoesSpeakerHaveLaterSessions(speaker, session);
                var timeUntilNextSession = GetTimeUntilNextSession(speaker, session);
                var timeSinceSubmission = TimeSinceSubmission(session);

                WriteLine($"Earlier sessions: {doesSpeakerHaveEarlierSessions}");
                WriteLine($"Later sessions: {doesSpeakerHaveLaterSessions}");
                WriteLine($"Time until next session: {timeUntilNextSession}");

                WriteLine($"Submitted at: {session.SubmittedAt}");
                WriteLine($"Days since submission: {Math.Abs(timeSinceSubmission.TotalDays)}"); 

                if(timeUntilNextSession != TimeSpan.MinValue &&
                    session.ScheduledAt.Add(session.Length).DayOfYear !=
                    session.ScheduledAt.Add(session.Length).Add(timeUntilNextSession).DayOfYear)
                {
                    WriteLine("-------------- NO MORE SESSIONS TODAY ---------------");
                }

                WriteLine();
                ReadLine();
            }
        }

        public static bool DoesSpeakerHaveEalierSessions(Speaker speaker, Session currentSession)
        {
            return speaker.Sessions.Any(
                session => session.Id != currentSession.Id &&
                session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Earlier
            );
        }

        public static bool DoesSpeakerHaveLaterSessions(Speaker speaker, Session currentSession)
        {
            return speaker.Sessions.Any(
                session => session.Id != currentSession.Id &&
                session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Later
            );
        }

        public static TimeSpan GetTimeUntilNextSession(Speaker speaker, Session currentSession)
        {
            var nextSession = speaker.Sessions.OrderBy(session => session.ScheduledAt)
                .FirstOrDefault(session => session.Id != currentSession.Id &&
                session.ScheduledAt >= currentSession.ScheduledAt);

            if(nextSession == null)
            {
                return TimeSpan.MinValue;
            }

            return nextSession.ScheduledAt - currentSession.ScheduledAt.Add(currentSession.Length);
        }

        public static TimeSpan TimeSinceSubmission(Session session)
        {
            var timeSinceSubmission = session.SubmittedAt - DateTimeOffset.UtcNow.ToOffset(session.SubmittedAt.Offset);

            return timeSinceSubmission;
        }




        public static Speaker GetSpeaker()
        {
            var speaker = new Speaker
            {
                Name = "Filip Ekberg",
                Birthday = new DateTime(1987, 01, 29),

                Sessions = new[] {
                    new Session
                    {
                        Title = "The state of C#",
                        Length = TimeSpan.FromMinutes(40),
                        SubmittedAt = new DateTimeOffset(2016, 02, 29, 00, 01, 00, TimeSpan.FromHours(1)),  // 2016-02-29 00:01:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))   // 2019-08-01 09:40:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "C# 8 and Beyond",
                        Length = TimeSpan.FromHours(1),
                        SubmittedAt = new DateTimeOffset(2016, 02, 29, 00, 00, 00, TimeSpan.FromHours(1)),  // 2016-02-29 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 11, 01, 00, TimeSpan.FromHours(2))   // 2019-08-01 11:01:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "Succeeding with Mobile Development",
                        Length = TimeSpan.FromMinutes(55),
                        SubmittedAt = new DateTimeOffset(2019, 01, 01, 00, 01, 00, TimeSpan.FromHours(1)),  // 2019-01-01 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 12, 00, 00, TimeSpan.FromHours(2))   // 2019-08-01 12:00:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "Using Dates and Times in .NET",
                        Length = new TimeSpan(01, 45, 00),
                        SubmittedAt = new DateTimeOffset(2019, 01, 01, 00, 00, 00, TimeSpan.FromHours(1)),  // 2019-01-01 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 02, 09, 00, 00, TimeSpan.FromHours(2))   // 2019-08-02 09:00:00.0000000 +02:00
                    }
                }
            };

            return speaker;
        }

        public enum DateComparison
        {
            Earlier = -1,
            Later = 1,
            TheSame = 0
        }
    }
}
