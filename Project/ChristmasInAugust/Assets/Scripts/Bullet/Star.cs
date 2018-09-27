using Utility;
using UnityEngine;

namespace Bullet
{
    // 먹을 수 있는 별
    public class Star : SpeedyBullet
    {
        #region Properties
        // protected float Score { get; set; } = 0;
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                if (Data.IsEasterEgg)
                    AddScore();

                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.CLOUD))
            {
                AddScore();
                PlayDestroySound();
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))
            {
                ShowParticle();
                
                if (Data.IsEasterEgg)
                    collision.gameObject.GetComponent<Player.Player>().Damage(Damage);
                else
                {
                    PlayDestroySound();
                    AddScore();
                }

                Destroy(gameObject);
            }
            //else
                //Debug.LogWarning("Another tag detected.");
        }
    }
}