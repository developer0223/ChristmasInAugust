using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;
using Manager;

namespace Bullet
{
    public class Heart : SpeedyBullet
    {
        private int HealAmount { get; set; } = 10;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLAYER))
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<Player.Player>().Heal(HealAmount);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}