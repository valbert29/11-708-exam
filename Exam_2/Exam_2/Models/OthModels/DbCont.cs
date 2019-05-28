using Exam_2.Models.OthModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_2.Models
{
    public class DbCont:DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Restoran> Restorans { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Promo> Promos { get; set; }
        public DbCont(DbContextOptions<DbCont> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            var adminLogin = "admin";
            var adminPass = adminLogin.GetHashCode().ToString();

            var adminPeriod = new DateTime(9999, 12, 20);
            var startAdmin = new DateTime();
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User
            {
                Id = 1,
                Login = adminLogin,
                Name = adminLogin,
                Password = adminPass,
                RoleId = adminRole.Id
            };
            modelBuilder.Entity<User>()
                .HasMany(e => e.Promos)
                .WithOne(x => x.User);

            var sub1 = new Promo { Id = 1, Name = "5%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub2 = new Promo { Id = 2, Name = "10%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub3 = new Promo { Id = 3, Name = "15%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub4 = new Promo { Id = 4, Name = "20%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub5 = new Promo { Id = 5, Name = "25%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub6 = new Promo { Id = 6, Name = "30%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub7 = new Promo { Id = 7, Name = "35%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            var sub8 = new Promo { Id = 8, Name = "40%Promo", StartTime = startAdmin, EndTime = adminPeriod, UserId = adminUser.Id };
            modelBuilder.Entity<User>()
                .HasMany(e => e.Promos)
                .WithOne(x => x.User)
                .IsRequired();
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Promo>().HasData(new Promo[] { sub1, sub2, sub3, sub4, sub5, sub6, sub7, sub8 });
            base.OnModelCreating(modelBuilder);
        }
    }    
}
