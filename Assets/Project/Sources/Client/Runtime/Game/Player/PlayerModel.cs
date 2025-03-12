using System;
using Client.Runtime.Game.Mechanics;
using Client.Runtime.Game.Mechanics.Inventory;
using Client.Runtime.Game.ScriptableObjects.Entities.PlayerCharacters;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class PlayerModel : MonoBehaviour
    {
        [Header("Stats")]
        public float MaxSpeed;
        public float JumpVelocity;
        public float BaseGravity;

        public PlayerCharacterScriptableObject scriptableObject;
        [NonSerialized] public CharacterStats currentStats = new();
        public GodBuffs godBuffs;

        private int hp;

        public Action OnDeath;
        public Action OnHpUpdate;


        public int HP { get => hp; set {
                hp = Mathf.Clamp(value, 0, currentStats.maxHp);
                OnHpUpdate?.Invoke();

                if (hp <= 0)
                {
                    OnDeath?.Invoke();
                }
            } 
        }

        public static GodBuffs MainPlayerGodBuffs { get; private set; }

        private void Awake() {
            godBuffs.Set(scriptableObject.godBuffs);
            currentStats.Set(scriptableObject.baseStats);
            MainPlayerGodBuffs = godBuffs;
            hp = scriptableObject.baseStats.maxHp;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.G))
            {
                HP -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                HP += 1;
            }
        }
    }
}