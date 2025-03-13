using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Runtime.Game.Mechanics.AttackInterfaces;
using Client.Runtime.Game.Player;
using Sources.Client.Runtime.Game.Mechanics;
using UnityEngine;

namespace Client.Runtime.Game.Mechanics.Bullets
{
    public class PlayerBulletSender : MonoBehaviour, IBulletSender
    {
        public void OnAttack(IHittable entity)
        {
            if (entity.GetName() == "Sans")
            {
                PlayerModel.MainPlayerGodBuffs.ChangeBuff(GodType.PAST, 0.01f);
            }
        }
    }
}