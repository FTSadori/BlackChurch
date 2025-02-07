using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Entities.PlayerCharacters
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Entities/Player")]
    public sealed class PlayerCharacterScriptableObject : ScriptableObject
    {
        public string characterName;
        public CharacterStats baseStats;
        public GodBuffs godBuffs;
    }
}