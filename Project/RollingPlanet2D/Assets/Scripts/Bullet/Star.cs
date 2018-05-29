using Player;
using Utility;
using UnityEngine;

namespace Bullet
{
    public class Star : SpeedyBullet
    {
        #region Properties
        // protected float Score { get; set; } = 0;
        #endregion

        void Start()
        {
            Speed = 250.0f;
            rigidbody2D.AddForce(direction.normalized * Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }

            if (collision.CompareTag(Data.Tags.PLAYER) ||
                collision.CompareTag(Data.Tags.CLOUD))
            {
                // audioSource.PlayOneShot(audioClip);
                AddScore();
                Destroy(gameObject);
            }
        }

        private void AddScore()
        {
            switch (gameObject.name)
            {
                // todo : 상속구조로 변경하기
                case "SilverStar(Clone)":
                    Data.Score.Snow += 3;
                    break;
                case "PinkStar(Clone)":
                    Data.Score.Snow += 4;
                    break;
                default:
                    Debug.Log($"diffferent tag : {gameObject.name}");
                    break;
            }
        }

    }
}