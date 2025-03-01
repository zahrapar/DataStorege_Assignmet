

using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;



public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    //public DbSet<ProductEntity> Products { get; set; }
    //public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    //public DbSet<UserEntity> Users { get; set; }

    public DbSet<ProjectEntity> Projects { get; set; }


    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    base.OnModelCreating(modelBuilder);

    //    // Add predefined status types to the database
    //    modelBuilder.Entity<StatusTypeEntity>().HasData(
    //        new StatusTypeEntity { Id = 1, StatusName = "Pending" },
    //        new StatusTypeEntity { Id = 2, StatusName = "In Progress" },
    //        new StatusTypeEntity { Id = 3, StatusName = "Completed" }
    //    );
    //}

}




