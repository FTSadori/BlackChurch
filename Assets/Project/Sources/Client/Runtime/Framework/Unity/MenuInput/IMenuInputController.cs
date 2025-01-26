using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;

namespace Client.Runtime.Framework.Unity.MenuInput
{
    public interface IMenuInputController
    {
        abstract public bool IsInputActive { get; set; }
        abstract public List<SerializableNotUpdateKeyDownCommand> KeyDownCommands { get; }
        abstract public SlotMenu GetAssociatedSlotMenu();
    }
}