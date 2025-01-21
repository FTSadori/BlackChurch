using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Game.Environment
{
    public interface ISelectable
    {
        abstract void Select();

        abstract void Deselect();

        abstract void Interact();
    }
}