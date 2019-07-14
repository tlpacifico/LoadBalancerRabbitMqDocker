using Microsoft.EntityFrameworkCore;


namespace Vote.Queue.DatabaseContext
{
    public class VoteContext : DbContext
    {
        public DbSet<VoteEntity> Vote { get; set; }      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=vote.db");
        }
       
    }
   
}
