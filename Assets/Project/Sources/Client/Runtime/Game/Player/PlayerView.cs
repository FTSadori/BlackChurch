using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Client.Runtime.Game.Player
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] Rigidbody2D _rigidbody;
        [SerializeField] Collider2D _collider;
        [SerializeField] SpriteRenderer _spriteRenderer;
        [SerializeField] SpriteRenderer _noHandsSprite;

        public Rigidbody2D Rigidbody => _rigidbody;
        public Collider2D Collider => _collider;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public SpriteRenderer NoHandsSprite => _noHandsSprite;
    }
}