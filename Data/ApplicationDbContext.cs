using Login.Domain;
using Microsoft.EntityFrameworkCore;

namespace Login.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Student> Student { get; set; } = default!;
        public virtual DbSet<Lesson> Lesson { get; set; } = default!;
        public virtual DbSet<Note> Note { get; set; } = default!;

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            string connectionString = root.GetSection("ConnectionStrings:DefaultConnection").Value;

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(p => p.Id);
            modelBuilder.Entity<Lesson>().HasKey(p => p.Id);
            modelBuilder.Entity<Note>().HasKey(p => p.Id);
        }

    }

}