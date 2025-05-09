﻿using Lab2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<TagModel> Tags { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CatalogModel> Catalogs { get; set; }
    public DbSet<CartModel> Carts { get; set; }
    public DbSet<CartItemModel> CartItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
