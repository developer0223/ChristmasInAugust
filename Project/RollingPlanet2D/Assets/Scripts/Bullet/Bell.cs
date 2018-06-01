using Manager;
using Utility;
using UnityEngine;

namespace Bullet
{
    public class Bell : SpeedyBullet
    {

        private void Start()
        {
            switch (gameObject.name)
            {
                // todo : 상속구조로 변경하기
                case "AngryBullet(Clone)":
                    Speed = 250.0f;
                    break;
                case "GrumpyBullet(Clone)":
                    Speed = 200.0f;
                    break;
                default:
                    Debug.Log($"diffferent tag : {gameObject.name}");
                    break;
            }
            rigidbody2D.AddForce(direction.normalized * Speed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                ShowParticle();
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
                    gameManager.GetOrCreateManager<SoundManager>().Play(destroySound, transform.position);
                }
                else
                {
                    Debug.Log($"Bell : damage : {Data.IsEasterEgg}");
                    collision.gameObject.GetComponent<Player.Player>().Damage(Damage);
                }
            }
            else if (collision.CompareTag(Data.Tags.CLOUD))
            {
                AddScore();
                Destroy(gameObject);
            }
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