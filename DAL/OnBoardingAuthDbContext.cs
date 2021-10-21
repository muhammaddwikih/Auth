using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class OnBoardingAuthDbContext : DbContext
    {
        public OnBoardingAuthDbContext(DbContextOptions<OnBoardingAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //add index here
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
