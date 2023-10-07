using GlowsBattlegrounds.CustomModels;

namespace GlowsBattlegrounds.Models;

public class Match
{
    public int id { get; set; }
    public string creationTime { get; set; }
    public string start { get; set; }
    public string end { get; set; }
    public string serverNumber { get; set; }
    public string mapName { get; set; }

    public List<PlayerDto> players { get; set; }


}