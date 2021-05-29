using System;
using System.Collections.Generic;

namespace Storing_Dates_and_Times_in_SQL_Server
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
