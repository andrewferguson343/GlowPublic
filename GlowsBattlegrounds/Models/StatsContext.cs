using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using GlowsBattlegrounds.Models;

public class StatsContext : DbContext
{
    public DbSet<PlayerStats> PlayerStats { get; set; }
    public DbSet<GlobalStats> GlobalStats { get; set; }
    public DbSet<GlobalStats> Audit { get; set; }

    public string DbPath { get; }
    

    public StatsContext(DbContextOptions<StatsContext> options)
        : base(options)
    { }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("data source=DESKTOP-V90O7SH;initial catalog=Glow;trusted_connection=true");
}



 