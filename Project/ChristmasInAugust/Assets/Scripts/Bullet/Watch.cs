using Utility;
using UnityEngine;

namespace Bullet
{
    public class Watch : SpeedyBullet
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLAYER) ||
                collision.CompareTag(Data.Tags.CLOUD))
            {
                Data.Item.SlowWatch++;
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }
        }
    }
}