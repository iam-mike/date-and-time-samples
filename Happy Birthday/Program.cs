using Shared;
using System;

using static System.Console;

namespace Happy_Birthday
{
    class Program
    {
        static void Main(string[] args)
        {
            var speaker = GetSpeaker();

            var age = GetSpeakerAge(speaker);

            WriteLine(age);

            var daysUntilBirthday = GetDaysUntilBirthday(speaker);

            if(daysUntilBirthday == 0)
            {
                WriteLine("Happy Birthday!");
            }
            else
            {
                WriteLine($"Don't forget to congratulate the speaker in {daysUntilBirthday} days!");
            }
        }

        public static int GetSpeakerAge(Speaker speaker)
        {
            var today = DateTimeOffset.UtcNow;

            var age = today.Year - speaker.Birthday.Year;

            if(speaker.Birthday.Date > today.Date.AddYears(-age))
            {
                age = age - 1;
            }

            return age;
        }

        public static int GetDaysUntilBirthday(Speaker speaker)
        {
            var today = DateTime.UtcNow.Date;
            var birthday = new DateTime(today.Year, speaker.Birthday.Month, 1);

            birthday = birthday.AddDays(speaker.Birthday.Day - 1);

            if(birthday < today)
            {
                birthday = new DateTime(today.Year + 1, speaker.Birthday.Month, 1);
                birthday = birthday.AddDays(speaker.Birthday.Day - 1);
            }

            return (int)(birthday - today).TotalDays;
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
    }
}
