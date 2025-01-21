using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class PlayerModel : MonoBehaviour
    {
        [Header("Stats")]
        public float MaxSpeed;
        public float JumpVelocity;
        public float BaseGravity;
    }
}