﻿using AngularMyApp.DataLayer.Entities.Access;
using AngularMyApp.DataLayer.Entities.Account;
using AngularMyApp.DataLayer.Entities.Product;
using AngularMyApp.DataLayer.Entities.Site;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularMyApp.DataLayer.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        #region Db Sets

        public DbSet<User> Users { get; set; }

        public DbSet<UserToken> UserToken{ get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Slider> Sliders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public DbSet<ProductVisit> ProductVisits { get; set; }
        #endregion

        #region disable cascading delete in database

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascades = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascades)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        #endregion

    }
}