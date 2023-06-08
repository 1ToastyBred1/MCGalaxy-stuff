using System;
using System.Threading;

using MCGalaxy;

public class CmdCount : Command
{
	public override string name { get { return "Count"; } }
	public override string shortcut { get { return ""; } }
	public override string type { get { return "other"; } }

	public override bool museumUsable { get { return true; } }

	public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }

	int count = 1;
    private bool running = false;
	
public override void Use(Player p, string message)
{
    if (message.CaselessEq("start"))
    {
        if (running)
        {
            p.Message("&cCount is already running.");
            return;
        }

        count = 1;
        running = true;
        while (running)
        {
            string count2 = count.ToString();
            try
            {
                p.SendCpeMessage(CpeMessageType.BottomRight1, "&dTimer: &a" + count2);
            }
            catch
            {
                running = false;
                return;
            }
            count++;
            Thread.Sleep(10);
        }
    }
    else if (message.CaselessEq("stop"))
    {
        if (!running)
        {
            p.Message("&cCount is not running.");
            return;
        }

        running = false;
        p.Message("&aStopped at: " + (count - 1));
        Thread.Sleep(2000);
        p.SendCpeMessage(CpeMessageType.BottomRight1, "");
    }
}



	public override void Help(Player p)
	{
		p.Message("/Count - Does stuff. Example command.");
	}
}