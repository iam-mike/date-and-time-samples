using System;

namespace Unix_Timestamp
{
    class Program
    {
        static void Main(string[] args)
        {
            var timestamp = 1562335678;

            // .NET Core version
            // var date = DateTimeOffset.FromUnixTimeSeconds(timestamp);

            var unixDateStart = new DateTime(1970, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            var date = unixDateStart.AddSeconds(timestamp);

            Console.WriteLine(new DateTimeOffset(date).ToUnixTimeSeconds());

            Console.WriteLine(date);
        }
    }
}
