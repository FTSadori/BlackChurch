using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.ScriptableObjects.Storages;
using UnityEngine;

namespace Client.Runtime.Game.ScriptableObjects.Entities.EnemyCharacters
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Entities/Enemy")]
    public sealed class EnemyCharacterScriptableObject : ScriptableObject
    {
        public string characterName;
        public CharacterStats baseStats;
        public StorageScriptableObject corpseObject;
    }
}