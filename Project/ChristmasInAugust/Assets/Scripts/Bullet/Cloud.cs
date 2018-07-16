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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLAYER) ||
                collision.CompareTag(Data.Tags.CLOUD))
            {
                itemManager.UseCloud();
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }
        }
    }
}