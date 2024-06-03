using Books;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.EFCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }

        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<MissionSkill> MissionSkills { get; set; }
        public DbSet<MissionTheme> MissionThemes { get; set; }
    }
}
