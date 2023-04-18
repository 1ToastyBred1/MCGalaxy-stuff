//Creator: Coconut1234, A.K.A: Naruto1234, Goldberg

using System;
using MCGalaxy;

public class CmdBigmodel : Command
{
    public override string name { get { return "Bigmodel"; } }
    public override string shortcut { get { return ""; } }
    public override string type { get { return "other"; } }

    public override bool museumUsable { get { return true; } }

    public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

    public override void Use(Player p, string message)
    {
        bool big = false;
        string botname = p.name;
        
        if (message.CaselessEq("on")) {
        Command.Find("bot").Use(p, "add " + botname);
        Command.Find("model").Use(p, "bot " + botname + " giant|5");
        Command.Find("model").Use(p, "0|0");
        big = true; 
        }
        if (message.CaselessEq("")) { 
            p.Message("&con/off!");
        }
        while (big) {
            Command.Find("botsummon").Use(p, botname);
            if (message.CaselessEq("off")) { 
            Command.Find("bot").Use(p, "remove " + botname);
            Command.Find("model").Use(p, "");
            big = false;
            }
        }
    }

    public override void Help(Player p)
    {
        p.Message("/Bigmodel - makes u big");
    }
}
