// [Key]
// public string steamId { get; set; }
// public string gamertag { get; set; }
// public string weaponKillsBlob { get; set; }
// public string weaponDeathsBlob { get; set; }
// public string mapBlob { get; set; }
// public int totalGames { get; set; }
// public double averageKd { get; set; }
// public double averageKpm { get; set; }
// public double averageDpm { get; set; }
// public int mostKills { get; set; }
// public int totalKills { get; set; }
// public int totalDeaths { get; set; }
// public int mostDeaths { get; set; }
// public int highestKillStreak { get; set; }
// public int highestDeathStreak { get; set; }
// public int timesBetrayed { get; set; }
// public string firstSeen { get; set; }
// public string lastSeen { get; set; }
// public int totalTeamkills { get; set; }

export type Player = {
    steamId: string;
    gamertag: string;
    weaponKillsBlob: string;
    weaponDeathsBlob: string;
    mapBlob: string;
    totalGames: number;
    averageKd: number;
    averageKpm: number;
    averageDpm: number;
    mostKills: number;
    totalKills: number;
    totalDeaths: number;
    mostDeaths: number;
    highestKillStreak: number;
    highestDeathStreak: number;
    timesBetrayed: number;
    firstSeen: string;
    lastSeen: string;
    totalTeamkills: number;
}