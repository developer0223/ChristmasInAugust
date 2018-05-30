using Player;
using Manager;
using Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bullet
{
    public class Cloud : SpeedyBullet
    {
        EffectManager effectManager;

        void Start()
        {
            effectManager = gameManager.GetOrCreateManager<EffectManager>();

            Speed = 100.0f;
            rigidbody2D.AddForce(direction.normalized * Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLAYER))
            {
                gameManager.MakeCloud();
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }
        }
    }
}