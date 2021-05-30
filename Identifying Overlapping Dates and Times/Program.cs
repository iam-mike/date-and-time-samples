using Shared;
using System;

namespace Identifying_Overlapping_Dates_and_Times
{
    class Program
    {
        static void Main(string[] args)
        {
            var speaker = GetSpeaker();

            Console.WriteLine($"Sessions for: {speaker.Name}");
            Console.WriteLine("---------------------");

            foreach (var session in speaker.Sessions)
            {
                Console.WriteLine(session.Title);
                Console.WriteLine($"Starts: {session.ScheduledAt}");
                Console.WriteLine($"Ends: {session.ScheduledAt.Add(session.Length)}");

                var overlap = GetOverlappingSessions(speaker, session);

                if (overlap != null)
                {
                    PrintWarning($"Overlapping with {overlap.Title}");
                }
                Console.ReadLine();
                Console.WriteLine();

            }
        }

        private static Session GetOverlappingSessions(Speaker speaker, Session currentSession)
        {
            var start = currentSession.ScheduledAt;
            var end = currentSession.ScheduledAt.Add(currentSession.Length);

            foreach (var session in speaker.Sessions)
            {
                if (session.Id == currentSession.Id) continue;

                if(session.ScheduledAt > start 
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

        public static void PrintWarning(string message)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message);

            Console.ResetColor();
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
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))   // 2019-08-01 09:40:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "C# 8 and Beyond",
                        Length = TimeSpan.FromHours(1),
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 11, 01, 00, TimeSpan.FromHours(2))   // 2019-08-01 11:01:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "Succeeding with Mobile Development",
                        Length = TimeSpan.FromMinutes(55),
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 12, 00, 00, TimeSpan.FromHours(2))   // 2019-08-01 12:00:00.0000000 +02:00
                    },
                    new Session
                    {
                        Title = "Using Dates and Times in .NET",
                        Length = new TimeSpan(01, 45, 00),
                        ScheduledAt = new DateTimeOffset(2019, 08, 02, 09, 00, 00, TimeSpan.FromHours(2))   // 2019-08-02 09:00:00.0000000 +02:00
                    }
                }
            };

            return speaker;
        }
    }
}
