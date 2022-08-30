using System.Data.Entity;


namespace WindowsFormsApp13
{
    class UserContext : DbContext
    {
        
        public UserContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
