using Utility;
using UnityEngine;

namespace Bullet
{
    public class Bell : SpeedyBullet
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET) || 
                collision.CompareTag(Data.Tags.CLOUD))
            {
                ShowParticle();
                PlayDestroySound();
                Destroy(gameObject);
                if (!Data.IsEasterEgg)
                {
                    AddScore();
                }
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))
            {
                Destroy(gameObject);
                if (Data.IsEasterEgg)
                {
                    AddScore();
                    PlayDestroySound();
                }
                else
                {
                    collision.gameObject.GetComponent<Player.Player>().Damage(Damage);
                }
            }
            else
            {
                Debug.LogWarning("Another tag detected.");
            }
        }
    }
}