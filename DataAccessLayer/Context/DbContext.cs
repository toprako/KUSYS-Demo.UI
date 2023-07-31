using EntityLayer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DataAccessLayer.Context
{
    public class DbContext : IdentityDbContext<User, UserRole, Guid>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=MERT-PC;Database=KUSYSDemoDb;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            Guid UserId = Guid.NewGuid(), RoleId = Guid.NewGuid();
            //Kurs Datalarını Database Oluştuğunda Kayıt Edilmesi 
            builder.Entity<Course>().HasData(
            new Course()
            {
                CourseId = "CSI101",
                CourseName = "Introduction to Computer Science"
            },
            new Course()
            {
                CourseId = "CSI102",
                CourseName = "Algorithms"
            },
            new Course()
            {
                CourseId = "MAT101",
                CourseName = "Calculus"
            },
            new Course()
            {
                CourseId = "PHY101",
                CourseName = "Physics"
            });

            //Kullanıcı Rollerini Database Oluştuğunda Kayıt Edilmesi
            builder.Entity<UserRole>().HasData(new UserRole()
            {
                Id = RoleId,
                Name = "Admin",
                NormalizedName = "Admin"
            },
            new UserRole()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "User"
            });

            //Admin Kullanıcısı Oluşturuldu
            builder.Entity<User>().HasData(new User()
            {
                Id = UserId,
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher().HashPassword("1234Test*"),
                UserName = "SuperAdmin",
                NormalizedUserName = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString()
            });

            builder.Entity<UserAndRole>().HasData(new UserAndRole()
            {
                UserId = UserId,
                RoleId = RoleId,
            });

            builder.Entity<CourseStudent>()
            .HasKey(hk => new { hk.StudentId, hk.CourseId });

            builder.Entity<CourseStudent>()
            .HasOne(hk => hk.Student)
            .WithMany(h => h.CourseStudent)
            .HasForeignKey(hk => hk.StudentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<CourseStudent>()
            .HasOne(hk => hk.Course)
            .WithMany(k => k.CourseStudent)
            .HasForeignKey(hk => hk.CourseId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAndRole> UserAndRoles { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
    }
}
