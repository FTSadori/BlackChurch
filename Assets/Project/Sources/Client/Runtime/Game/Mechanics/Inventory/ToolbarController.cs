using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.UI;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Inventory
{
    public sealed class ToolbarController : MonoBehaviour
    {
        [SerializeField] private ItemSlotController[] _slots = new ItemSlotController[7];
    }
}