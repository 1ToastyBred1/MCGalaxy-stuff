using System;
using MCGalaxy;

public class CmdShortname : Command
{
    public override string name { get { return "Shortname"; } }
    public override string shortcut { get { return "shortnick"; } }
    public override string type { get { return "other"; } }
    public override bool museumUsable { get { return true; } }
    
    public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
    
    public override void Use(Player p, string message)
    {
        if (message.Length == 0) 
        {
            PlayerOperations.SetNick(p, p.name, p.color + p.name);
            return; 
        }
	        
        string nick = Colors.StripUsed(message);
        	        
        if (nick.Length < 3)
        {
            p.Message("Shortened name must be 3 characters or more.");
            return;
        }
        	        
        if (!p.name.ToLower().Contains(nick.ToLower()))
        {
            p.Message("Your shortened name must be a part of your original name.");
            return;
        }
        	        
        PlayerOperations.SetNick(p, p.name, message);
    }

    public override void Help(Player p)
    {
        p.Message("&a/Shortname [New nick]");
        p.Message("Allows you to shorten your name. Your new name must be a part of your original name. You are allowed to add colors.");
    }
}
