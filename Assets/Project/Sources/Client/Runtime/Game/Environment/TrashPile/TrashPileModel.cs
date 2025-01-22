using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.ScriptableObjects.Piles;
using UnityEngine;

namespace Client.Runtime.Game.Environment.TrashPile
{
    public enum TrashPileState
    {
        AWAITS,
        BUSY,
        FOUND,
    }

    public sealed class TrashPileModel : MonoBehaviour
    {
        public TrashPileScriptableObject _trashPileScriptableObject;
        public TrashPileState _state = TrashPileState.AWAITS;
    }
}