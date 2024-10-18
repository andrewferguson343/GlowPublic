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
    
}



 