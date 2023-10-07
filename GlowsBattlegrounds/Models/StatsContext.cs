using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class StatsContext : DbContext
{
    // public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public string DbPath { get; }

    public StatsContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("data source=DESKTOP-V90O7SH;initial catalog=Glow;trusted_connection=true");
}

public class Player
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post> Posts { get; } = new();
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int BlogId { get; set; }
    // public Blog Blog { get; set; }
}

   // {
   //              "id": 3238806,
   //              "player_id": 142951,
   //              "steam_id_64": "76561198076037013",
   //              "player": "Nuwanda",
   //              "steaminfo": {
   //                  "id": 140782,
   //                  "created": "2021-11-18T18:38:54.449",
   //                  "updated": "2023-05-24T21:34:18.053",
   //                  "profile": {
   //                      "avatar": "https://avatars.steamstatic.com/67e178e49794f031d31cf782c27d0dba23324622.jpg",
   //                      "steamid": "76561198076037013",
   //                      "avatarfull": "https://avatars.steamstatic.com/67e178e49794f031d31cf782c27d0dba23324622_full.jpg",
   //                      "avatarhash": "67e178e49794f031d31cf782c27d0dba23324622",
   //                      "profileurl": "https://steamcommunity.com/profiles/76561198076037013/",
   //                      "personaname": "Nuwanda",
   //                      "timecreated": 1353201586,
   //                      "avatarmedium": "https://avatars.steamstatic.com/67e178e49794f031d31cf782c27d0dba23324622_medium.jpg",
   //                      "locstatecode": "BC",
   //                      "personastate": 0,
   //                      "profilestate": 1,
   //                      "primaryclanid": "103582791429521408",
   //                      "loccountrycode": "CA",
   //                      "personastateflags": 0,
   //                      "communityvisibilitystate": 3
   //                  },
   //                  "country": "CA",
   //                  "bans": null
   //              },
   //              "map_id": 30000,
   //              "kills": 8,
   //              "kills_streak": 4,
   //              "deaths": 4,
   //              "deaths_without_kill_streak": 2,
   //              "teamkills": 0,
   //              "teamkills_streak": 0,
   //              "deaths_by_tk": 0,
   //              "deaths_by_tk_streak": 0,
   //              "nb_vote_started": 0,
   //              "nb_voted_yes": 0,
   //              "nb_voted_no": 0,
   //              "time_seconds": 1508,
   //              "kills_per_minute": 0.32,
   //              "deaths_per_minute": 0.16,
   //              "kill_death_ratio": 2.0,
   //              "longest_life_secs": 428,
   //              "shortest_life_secs": 207,
   //              "combat": 67,
   //              "offense": 80,
   //              "defense": 320,
   //              "support": 0,
   //              "most_killed": {
   //                  "JTR": 1,
   //                  "Spye": 1,
   //                  "Ticket": 1,
   //                  "ThugPepe": 1,
   //                  "RoMpErRo0M": 1,
   //                  "Whiskey187": 1,
   //                  "AdmiralKarl": 1,
   //                  "Militaryhistory1": 1
   //              },
   //              "death_by": {
   //                  "tmas09": 1,
   //                  "CageNicholas": 1,
   //                  "StarvinPilgrim": 1,
   //                  "Krazykasualties": 1
   //              },
   //              "weapons": {
   //                  "LUGER P08": 1,
   //                  "KARABINER 98K": 1,
   //                  "150MM HOWITZER [sFH 18]": 6
   //              },
   //              "death_by_weapons": {
   //                  "PIAT": 1,
   //                  "Lanchester": 1,
   //                  "Rifle No.5 Mk I": 1,
   //                  "Lee-Enfield Pattern 1914 Sniper": 1
   //              }
   //          },