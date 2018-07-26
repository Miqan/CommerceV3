using System;
using System.Collections.Generic;
using System.Text;
using CommerceV3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommerceV3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

		public DbSet<CommerceV3.Models.Brand> Brands { get; set; }
		public DbSet<CommerceV3.Models.Cart> Carts { get; set; }
		public DbSet<CommerceV3.Models.CartItem> CartItems { get; set; }
		public DbSet<CommerceV3.Models.Category> Categories { get; set; }
		public DbSet<CommerceV3.Models.Menu> Menus { get; set; }
		public DbSet<CommerceV3.Models.MenuItem> MenuItems { get; set; }
		public DbSet<CommerceV3.Models.Product> Products { get; set; }
		public DbSet<CommerceV3.Models.ProductCategory> ProductCategories { get; set; }
		public DbSet<CommerceV3.Models.Region> Regions { get; set; }
		public DbSet<CommerceV3.Models.Slide> Slides { get; set; }
		public DbSet<CommerceV3.Models.Supplier> Suppliers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// fluent-API
			builder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });
		}
	}
}
