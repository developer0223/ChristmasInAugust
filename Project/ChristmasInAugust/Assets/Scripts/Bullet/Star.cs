using Utility;
using UnityEngine;

namespace Bullet
{
    public class Star : SpeedyBullet
    {
        #region Properties
        // protected float Score { get; set; } = 0;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET) ||
                collision.CompareTag(Data.Tags.CLOUD))
            {
                Destroy(gameObject);

                if (Data.IsEasterEgg)
                {
                    AddScore();
                }
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))
            {
                if (Data.IsEasterEgg)
                {
                    collision.gameObject.GetComponent<Player.Player>().Damage(Damage);
                }
                else
                {
                    AddScore();
                    ShowParticle();
                    PlayDestroySound();
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Another tag detected.");
            }
        }
    }
}