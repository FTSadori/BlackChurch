using System.Diagnostics.Contracts;

namespace Client.Runtime.System
{
    public sealed class Scenes
    {
        public static class System
        {
            public const string Input = "System_Input";
            public const string MainCamera = "System_MainCamera";
			public const string Audio = "System_Audio";
        }

        public static class Activity
        {
            public const string Lobby = "Activity_Lobby";
            public const string Game = "Activity_Game";
            public const string Room = "Activity_Room";
        }
    }
}