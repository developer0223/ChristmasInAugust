using UnityEngine;
using System.Collections;

namespace Enemy
{
    public abstract class Turret : MonoBehaviour
    {
        public GameObject[] bullets = new GameObject[4];

        protected float bulletDelayTime = 0.2f;
        protected GameObject currentBullet;
        protected float radius = 20.0f;

        protected IEnumerator EShootCoroutine;

        public enum Bullet
        {
            AngryBullet,
            GrumpyBullet,
            YellowStar,
            SilverStar,
            Random
        }

        public void Shoot(float playTime, Bullet bullet)
        {
            SetBullet(bullet);
            StartCoroutine(EShoot(playTime, bullet));
        }

        protected virtual IEnumerator EShoot(float playTime, Bullet bullet)
        {
            Debug.Log("You may override EShoot method.");
            yield return null;
        }

        protected float GetX(float degree)
        {
            return Mathf.Cos(degree * Mathf.Deg2Rad) * radius;
        }

        protected float GetY(float degree)
        {
            return Mathf.Sin(degree * Mathf.Deg2Rad) * radius;
        }

        protected Vector3 GetPosition(float degree)
        {
            float x = Mathf.Cos(degree * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(degree * Mathf.Deg2Rad) * radius;

            return new Vector3(x, y, 0);
        }

        protected void SetBullet(Bullet bullet)
        {
            GameObject obj = null;
            switch (bullet)
            {
                case Bullet.AngryBullet:
                    obj = bullets[0];
                    break;
                case Bullet.GrumpyBullet:
                    obj = bullets[1];
                    break;
                case Bullet.YellowStar:
                    obj = bullets[2];
                    break;
                case Bullet.SilverStar:
                    obj = bullets[3];
                    break;
                case Bullet.Random:
                    obj = bullets[Random.Range(0, bullets.Length)];
                    break;
            }
            currentBullet = obj;
        }
    }
}