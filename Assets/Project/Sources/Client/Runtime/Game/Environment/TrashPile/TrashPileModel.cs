using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public float _timeToFindItem = 2f;
        public TrashPileState _state = TrashPileState.AWAITS;
    }
}