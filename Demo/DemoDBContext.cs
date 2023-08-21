using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Demo.DataModel;

public class DemoContext : DbContext
{
    public DbSet<Security> Security { get; set; }
    //public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public DemoContext()
    {
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Directory.GetCurrentDirectory();
        DbPath = System.IO.Path.Join(path, "demo.db");
        Database.EnsureCreated();
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}