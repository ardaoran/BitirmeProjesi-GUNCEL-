﻿using BitirmeProjesi.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitirmeProjesi.DAL.Content
{
    public class SQLContext:IdentityDbContext<AppUser,AppRole,int>
    {
        public SQLContext(DbContextOptions<SQLContext> opt) : base(opt)
        {

        } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

            modelBuilder.Entity<ProductPicture>().HasOne(x => x.Product).WithMany(x => x.ProductPictures);

            modelBuilder.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                ID = 1,
                NameSurname = "Arda Oran",
                LastLongDate = DateTime.Now,
                LastLoginIP="",
                UserName = "arda",
                Password = "202cb962ac59075b964b07152d234b70",
            });
            
            base.OnModelCreating(modelBuilder); 
                
        }

       

        public DbSet<Admin> Admin { get; set;}
        public DbSet<Slide> Slide { get; set;}
        public DbSet<Category> Category { get; set;}
        public DbSet<Product> Product { get; set;}
        public DbSet<ProductPicture> ProductPicture { get; set;}
        public DbSet<Brand> Brand { get; set;}
        public DbSet<Order> Order { get; set;}
        public DbSet<OrderDetails> OrderDetails { get; set;}
            
        

    }
}
