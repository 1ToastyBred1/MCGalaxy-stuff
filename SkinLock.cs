using System;
using MCGalaxy.Events.PlayerEvents;

namespace MCGalaxy {
  public class SkinLock: Plugin {
    public override string name { get { return "SkinLock"; } }
    public override string MCGalaxy_Version { get { return "1.9.4.9"; } }
    public override string welcome { get { return "Loaded Message!"; } }
    public override string creator { get { return "Goldberg"; } }

    public override void Load(bool startup) {
      OnPlayerCommandEvent.Register(HandleCommand, Priority.Low);
      OnSendingMotdEvent.Register(HandleGettingMotd, Priority.Low);
    }

    public override void Unload(bool shutdown) {
      OnPlayerCommandEvent.Unregister(HandleCommand);
      OnSendingMotdEvent.Unregister(HandleGettingMotd);
    }

    void HandleCommand(Player p, string cmd, string args, CommandData data) {
      if (cmd != "skin") return;
      if (!p.GetMotd().CaselessContains("skin=")) return;

      if (!args.CaselessContains(GetSkin(p))) {
        p.cancelcommand = true;
        p.Message("&cYou are not allowed to use any other skin!");
      }
    }

    void HandleGettingMotd(Player p, ref string motd) {
      if (!motd.Contains("skin=")) return;

      string skin = GetSkin(p);
      Command.Find("skin").Use(p, skin);
    }

    string GetSkin(Player p) {
      string skin = "";
      string motd = p.GetMotd();

      if (motd.CaselessContains("skin=")) {
        foreach(string arg in motd.Split(" ")) {
          if (arg.CaselessStarts("skin=")) {
            skin = arg.Replace("skin=", "");
          }
        }
      }
      return skin;
    }

    public override void Help(Player p) {
      p.Message("Locks skin to the one request in motd.");
    }
  }
}
