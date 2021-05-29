using Microsoft.EntityFrameworkCore;

namespace Storing_Dates_and_Times_in_SQL_Server
{
    public class SessionBuilderContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SessionBuilder;Trusted_Connection=True;ConnectRetryCount=0;");
        }
    }    
}
