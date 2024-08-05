using System;
using MCGalaxy.Events.PlayerEvents;

namespace MCGalaxy
{
	public class NonRepetiveMessages : Plugin
	{
		public override string name { get { return "NonRepetiveMessages"; } }
		public override string MCGalaxy_Version { get { return "1.9.4.9"; } }
		public override string welcome { get { return "Loaded Message!"; } }
		public override string creator { get { return "ToastyBred"; } }
	
	
		public override void Load(bool startup)
		{
			OnPlayerChatEvent.Register(HandlePlayerChat, Priority.Normal);
		}
	
	
		public override void Unload(bool shutdown)
		{
			OnPlayerChatEvent.Unregister(HandlePlayerChat);
		}
	        
		void HandlePlayerChat(Player p, string message)
		{
			if (!p.Extras.Contains("lastMessage")) { p.Extras["lastMessage"] = string.Empty; return; }
	            
			if ((string)p.Extras["lastMessage"] == message)
			{
				p.Message("Please do not send repetitive messages.");
				p.cancelchat = true;
			}
			p.Extras["lastMessage"] = message;
		}
	
	
		public override void Help(Player p)
		{
			p.Message("No help is available for this plugin.");
		}
	}
}
