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
            //var a = new DateTimeOffset(2016, 03, 28, 00, 01, 00, TimeSpan.FromHours(1));
            //var b = new DateTimeOffset(2016, 02, 29, 00, 01, 00, TimeSpan.FromHours(1));

            //WriteLine(a.CompareTo(b));

            var speaker = GetSpeaker();

            WriteLine($"Sessions for: {speaker.Name}");
            WriteLine("-----------------------------");

            foreach (var session in speaker.Sessions)
            {
                Console.WriteLine(session.Title);
                Console.WriteLine($"Starts: {session.ScheduledAt}");
                Console.WriteLine($"Ends: {session.ScheduledAt.Add(session.Length)}");

                var earlierSessions = DoesSpeakerHaveEarlierSessions(speaker, session);
                var laterSessions = DoesSpeakerHaveLaterSessions(speaker, session);
                var coffeeBreak = GetTimeUntilNextSession(speaker, session);
                var submissionTime = TimeSinceSubmission(session);


                Console.WriteLine($"EarlierSessions: {earlierSessions}");
                Console.WriteLine($"Later Sessions: {laterSessions}");
                Console.WriteLine($"Coffee Break: {coffeeBreak}");
                Console.WriteLine($"Time since submission: {Math.Abs(submissionTime.TotalDays)}");


                if (coffeeBreak != TimeSpan.MinValue
                    && session.ScheduledAt.Add(session.Length).DayOfYear != 
                    session.ScheduledAt.Add(session.Length).Add(coffeeBreak).DayOfYear)
                {
                    Console.WriteLine("------------ NO MORE SESSIONS TODAY ------------");
                }

                Console.ReadLine();
                Console.WriteLine();
            }

        }

        private static TimeSpan TimeSinceSubmission(Session session)
        {
            var timeSince = session.SubmittedAt - DateTimeOffset.UtcNow.ToOffset(session.SubmittedAt.Offset);

            return timeSince;
        }

        private static TimeSpan GetTimeUntilNextSession(Speaker speaker, Session currentSession)
        {
            var nextSession = speaker.Sessions.OrderBy(s => s.ScheduledAt)
                .FirstOrDefault(s => s.Id != currentSession.Id &
                s.ScheduledAt >= currentSession.ScheduledAt);

            if(nextSession == null)
            {
                return TimeSpan.MinValue;
            }

            return nextSession.ScheduledAt - currentSession.ScheduledAt.Add(currentSession.Length);
        }

        private static bool DoesSpeakerHaveEarlierSessions(Speaker speaker, Session currentSession)
        {
            return speaker.Sessions.Any(
                session => session.Id != currentSession.Id &&
                session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Earlier
                );
        }

        private static bool DoesSpeakerHaveLaterSessions(Speaker speaker, Session currentSession)
        {
            return speaker.Sessions.Any(
                session => session.Id != currentSession.Id &&
                session.ScheduledAt.CompareTo(currentSession.ScheduledAt) == (int)DateComparison.Later
                );
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
        private static Session GetOverlappingSessions(Speaker speaker, Session currentSession)
        {
            var start = currentSession.ScheduledAt;
            var end = currentSession.ScheduledAt.Add(currentSession.Length);

            foreach (var session in speaker.Sessions)
            {
                if (session.Id == currentSession.Id) continue;

                if (session.ScheduledAt > start
                    && session.ScheduledAt < end)
                {
                    return session;
                }
                if (session.ScheduledAt.Add(session.Length) > start
                    && session.ScheduledAt.Add(session.Length) < end)
                {
                    return session;
                }

            }
            return null;
        }
        public enum DateComparison
        {
            Earlier = -1,
            Later = 1,
            TheSame = 0
        }
    }
}
