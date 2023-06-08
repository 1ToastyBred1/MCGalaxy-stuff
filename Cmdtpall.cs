// Can be hacky. similiar to /foreach cmd in NA2

using System;
using System.Threading;

using MCGalaxy;

public class CmdTpall : Command
{
	public override string name { get { return "Tpall"; } }
	public override string shortcut { get { return ""; } }
	public override string type { get { return "other"; } }

	public override bool museumUsable { get { return true; } }
    
	public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

	public override void Use(Player p, string message)
	{
        try
        {
        int blockToTP = Convert.ToInt32(message);
        for (ushort x = 0; x <= p.level.Width; x++) {
            for (ushort y = 0; y <= p.level.Height; y++) {
                for (ushort z = 0; z <= p.level.Length; z++) {
                    ushort block = p.level.GetBlock(x, y, z);
                    if (block == blockToTP) {
                        Teleport(p, x, y, z);
                        }
                    }
                }
            }
        }
        catch (FormatException)
        {
            p.Message("&cYou must input an integer!");
        }

     }
    
    void Teleport(Player p, int x, int y, int z) {
        byte yaw = 0;
        byte pitch = 0;
        
        x = (ushort)x * 32 + 16;
        y = (ushort)y * 32 + 50;
        z = (ushort)z * 32 + 16;
        
        Position pos = Position.FromFeet((ushort)x, (ushort)y, (ushort)z);
        Orientation ori = new Orientation(yaw, pitch);
        p.SendPosition(pos, ori);
    }
    
    void Teleport_onSlab(Player p, int x, int y, int z) {
        byte yaw = 0;
        byte pitch = 0;
        
        x = (ushort)x * 32 + 16;
        y = (ushort)y * (32 / 2) + 50;
        z = (ushort)z * 32 + 16;
        
        Position pos = Position.FromFeet((ushort)x, (ushort)y, (ushort)z);
        Orientation ori = new Orientation(yaw, pitch);
        p.SendPosition(pos, ori);
    }

	public override void Help(Player p)
	{
		p.Message("&a/Tpall &f- [block].");
		p.Message("&aSearch for a [block] and teleports to it.");
	}
}