using System;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace RestaurantApi.Infrastructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext

    {
        //public DbSet<User> Users { get; set; }


        // constructor
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent API
            base.OnModelCreating(modelBuilder);

            #region tables
            //modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region primary-keys
            //modelBuilder.Entity<User>().HasKey(user => user.Id);
            #endregion

            #region relationships
            // User con Post
            //modelBuilder.Entity<User>()
            //   .HasMany(u => u.Posts)
            //   .WithOne(p => p.User)
            //   .HasForeignKey(p => p.UserId)
            //   .OnDelete(DeleteBehavior.Cascade);
            #endregion

           #region property configurations
                #region User
                //modelBuilder.Entity<User>(entity =>
                //{
                //    entity.Property(e => e.Name)
                //            .IsRequired()
                //            .HasMaxLength(100);

                //    entity.Property(e => e.LastName)
                //             .IsRequired()
                //             .HasMaxLength(100);

                //    entity.Property(e => e.Phone)
                //            .IsRequired()
                //            .HasMaxLength(15);

                //    entity.Property(e => e.ImageUrl);

                //    entity.Property(e => e.Email)
                //            .IsRequired()
                //            .HasMaxLength(100);

                //    entity.Property(e => e.UserName)
                //            .IsRequired()
                //            .HasMaxLength(50);

                //    entity.Property(e => e.Password)
                //            .IsRequired();

                //    entity.Property(e => e.IsActive)
                //            .IsRequired();

                //    entity.Property(e => e.ActivationToken);
                //});
                 #endregion

            #endregion
        }

    }
}

