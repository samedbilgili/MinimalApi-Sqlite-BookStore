using Microsoft.EntityFrameworkCore;

namespace MinimalApi.Models
{
    class DataContext : DbContext
    {
        public DbSet<Books> Books {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

    }

}