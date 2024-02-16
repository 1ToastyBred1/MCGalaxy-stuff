using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MCGalaxy.Events.PlayerEvents;

namespace MCGalaxy
{
	public class LastLocation : Plugin
	{
		public override string name { get { return "LastLocation"; } }
		public override string MCGalaxy_Version { get { return "1.9.4.9"; } }
		public override string welcome { get { return "Loaded Message!"; } }
		public override string creator { get { return "Goldberg"; } }
		
        string folder = "LastLocations";
        
		public override void Load(bool startup)
		{
        	if (!Directory.Exists(folder)) 
            {
            	Directory.CreateDirectory(folder);
            }
            
        	OnPlayerConnectEvent.Register(HandlePlayerConnect, Priority.Low);
        	OnPlayerDisconnectEvent.Register(HandlePlayerDisconnect, Priority.Low);
		}

		public override void Unload(bool shutdown)
        {
			OnPlayerConnectEvent.Unregister(HandlePlayerConnect);
			OnPlayerDisconnectEvent.Unregister(HandlePlayerDisconnect);
		}
        
        async void HandlePlayerConnect(Player p) {
            await Task.Run(() => {
            	Thread.Sleep(1000);
                try{
                    string info = File.ReadAllText(folder + "/" + p.name);
                    string[] args = info.Split(" ");
                    if (p.level.name != args[5]){
                        PlayerActions.ChangeMap(p, args[5]);
                    }
                    Thread.Sleep(500);
                    Teleport(p, int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]), byte.Parse(args[3]), byte.Parse(args[4]));
                } catch(Exception){}
            });
        }
        
        void HandlePlayerDisconnect(Player p, string reason) {
            int x = (p.Pos.X - 32) / 32;
            int y = (p.Pos.Y - 32) / 32;
            int z = (p.Pos.Z - 32) / 32; z++;
            byte yaw = p.Rot.RotY;
            byte pitch = p.Rot.HeadX;
            
            File.WriteAllText(folder + "/" + p.name, string.Format("{0} {1} {2} {3} {4} {5}", x, y, z, yaw, pitch, p.level.name));
        }
        void Teleport(Player p, int x, int y, int z, byte yaw, byte pitch) {
            x = (ushort)x * 32 + 16;
            y = (ushort)y * 32 + 51;
            z = (ushort)z * 32 + 16;

            Position pos = Position.FromFeet((ushort)x, (ushort)y, (ushort)z);
            Orientation ori = new Orientation(yaw, pitch);
            p.SendPosition(pos, ori);
    	}

		public override void Help(Player p)
		{
			p.Message("");
		}
	}
}
