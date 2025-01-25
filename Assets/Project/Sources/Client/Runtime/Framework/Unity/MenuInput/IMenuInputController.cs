using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;

namespace Client.Runtime.Framework.Unity.MenuInput
{
    public interface IMenuInputController
    {
        abstract public SlotMenu GetAssociatedSlotMenu();
        abstract public void CheckInput();
    }
}