using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI.AnimationControllers;
using UnityEngine;

namespace Client.Runtime.Game.Environment
{
    public enum InteractType
    {
        First,
        Second,
    }

    public interface ISelectable
    {
        abstract HelpBoxType HelpBoxType { get; }

        abstract void Select();

        abstract void Deselect();

        abstract void Interact(InteractType type);
    }
}