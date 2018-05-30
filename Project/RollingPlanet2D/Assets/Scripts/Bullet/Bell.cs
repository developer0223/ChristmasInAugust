using Player;
using Utility;
using UnityEngine;

namespace Bullet
{
    public class Bell : SpeedyBullet
    {

        private void Start()
        {
            Speed = 250.0f;
            rigidbody2D.AddForce(direction.normalized * Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                ShowParticle();
                AddScore();
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))
            {
                collision.gameObject.GetComponent<SnowMan>().Damage(Damage);
            }
            else if (collision.CompareTag(Data.Tags.CLOUD))
            {
                AddScore();
            }

            Destroy(gameObject);
        }

        private void AddScore()
        {
            switch (gameObject.name)
            {
                // todo : 상속구조로 변경하기
                case "AngryBullet(Clone)":
                    Data.Score.Avoid += 1;
                    break;
                case "GrumpyBullet(Clone)":
                    Data.Score.Avoid += 2;
                    break;
                default:
                    Debug.Log($"diffferent tag : {gameObject.name}");
                    break;
            }
        }
    }
}