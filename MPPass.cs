#if !Supernova
#define MCGalaxy
#endif

using System;
#if MCGalaxy
using MCGalaxy;
using MCGalaxy.Network;
#endif
#if Supernova
using Supernova;
#endif

namespace Core {
    public class GetMPPass : Plugin {
#if MCGalaxy
        public override string MCGalaxy_Version { get { return "1.9.3.5"; } }
#endif
#if Supernova
        public override string Supernova_Version { get { return "1.0.0.0"; } }
#endif
        public override string name { get { return "MPPass"; } }

        public override void Load(bool startup) {
            Command.Register(new CmdMPPass());
        }

        public override void Unload(bool shutdown) {
            Command.Unregister(Command.Find("MPPass"));
        }
    }

    public class CmdMPPass : Command2 {
        public override string name { get { return "MPPass"; } }
        public override string type { get { return "other"; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }

        public override bool LogUsage { get { return false; } }
        public override bool UpdatesLastCmd { get { return false; } }
        public override bool MessageBlockRestricted { get { return true; } }

        public override void Use(Player p, string message) {
            if (message == "") {
                Help(p);
                return;
            }

#if MCGalaxy
            if (message == "-salt") {
                foreach (Heartbeat hb in Heartbeat.Heartbeats) {
                    p.Message("The current salt is {0} for {1}", hb.Auth.Salt, hb.URL);
                }
            } else {
                foreach (Heartbeat hb in Heartbeat.Heartbeats) {
                    p.Message("MPPass for {0} is {1} on {2}",
                        message,
                        Server.CalcMppass(message, hb.Auth.Salt),
                        hb.URL);
                }
#endif

#if Supernova
            p.Message("Classicube MPPass for {0} is {1}",
                message,
                Server.CalcMppass(message, Server.salt));
            p.Message("Betacraft MPPass for {0} is {1}",
                message,
                Server.CalcMppass(message, Server.betacraftSalt));
#endif

#if MCGalaxy
            }
#endif
        }

        public override void Help(Player p) {
            p.Message("%T/MPPass -- Calculate current mppass for ID");
            p.Message("Use %T/MPPass -salt%S for server secret");
        }
    }
}
