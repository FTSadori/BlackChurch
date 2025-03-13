using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Environment.Storage;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using Client.Runtime.Game.ScriptableObjects.Entities.EnemyCharacters;
using Client.Runtime.Game.ScriptableObjects.Storages;
using Client.Runtime.Game.UI.Commands.InputCommands;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Bullets
{
    public class BasicEntityDataHolder : AbstractEntityDataHolder
    {
        [SerializeField] GameObject parent;
        [SerializeField] EnemyCharacterScriptableObject enemyObject;
        [SerializeField] GameObject baseCorpsePrefab;
        [SerializeField] OpenStorageMenuInputCommand openStorageMenuInputCommand;

        int hp;

        void Awake()
        {
            hp = enemyObject.baseStats.maxHp;
        }

        public override void ChangeHp(float value, float pure)
        {
            value = value * 100 / (100 + enemyObject.baseStats.baseDefence) - enemyObject.baseStats.pureDefence + pure;

            hp += (int)value;
            if (hp > enemyObject.baseStats.maxHp)
                hp = enemyObject.baseStats.maxHp;

            if (hp <= 0)
            {
                OnDeath();
            }

            Debug.Log(hp + " hp");
        }

        public override void OnDeath()
        {
            if (enemyObject.corpseObject != null)
            {
                var obj = Instantiate(baseCorpsePrefab);
                obj.transform.position = transform.position;
                obj.GetComponentInChildren<StorageModel>()._storageScriptableObject = enemyObject.corpseObject;
                obj.GetComponentInChildren<StorageController>()._name = enemyObject.characterName;    
                obj.GetComponentInChildren<StorageController>()._openStorageMenuInputCommand = openStorageMenuInputCommand;
            }

            Destroy(parent);
        }

        public override CharacterStats GetCharacterStats()
        {
            return enemyObject.baseStats;
        }

        public override string GetName()
        {
            return enemyObject.characterName;
        }
    }
}