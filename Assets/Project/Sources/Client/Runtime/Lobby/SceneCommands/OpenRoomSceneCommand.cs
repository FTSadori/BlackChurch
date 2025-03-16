using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Framework.Unity.SceneCommands;
using Client.Runtime.System;

namespace Client.Runtime.Lobby.SceneCommands
{
    public sealed class OpenRoomSceneCommand : ManageScenesCommand
    {
        protected override string SceneToLoadAndActivate => Scenes.Activity.Room;

        protected override List<string> ScenesToUnload => new() { Scenes.Activity.Lobby };

        protected override List<string> ScenesToLoad => new() {};
    }
}