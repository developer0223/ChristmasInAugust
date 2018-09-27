using Utility;
using UnityEngine;

namespace Bullet
{
    // 피해야 하는 별
    public class Bell : SpeedyBullet
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                if (!Data.IsEasterEgg)
                {
                    AddScore();
                    ShowParticle();
                }

                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.CLOUD))
            {
                if (Data.IsEasterEgg)
                    AddScore();

                PlayDestroySound();
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))
            {
                if (Data.IsEasterEgg)
                {
                    AddScore();
                    PlayDestroySound();
                }
                else
                    collision.gameObject.GetComponent<Player.Player>().Damage(Damage);

                Destroy(gameObject);
            }
            //else
            //    Debug.LogWarning("Another tag detected.");
        }
    }
}