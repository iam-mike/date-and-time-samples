using System;
using System.Collections.Generic;

namespace Shared
{
    public class Speaker
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public IEnumerable<Session> Sessions { get; set; }
    }
}
