using Microsoft.EntityFrameworkCore;


namespace Labb4Sofia_Lindgren.Data
{
    public class Context : DbContext
    {
        public DbSet<Account> Account { get; set; }
        private string URI = "https://localhost:8081";
        private string key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private string database = "Account";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(URI, key, database);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
