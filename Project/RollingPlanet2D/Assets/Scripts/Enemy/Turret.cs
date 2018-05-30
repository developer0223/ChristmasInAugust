using Manager;
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

        protected GameManager gameManager;
        protected CalculateManager calculateManager;

        protected IEnumerator EShootCoroutine;

        public enum Bullet
        {
            AngryBullet,
            GrumpyBullet,
            YellowStar,
            SilverStar,
            Random
        }

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            calculateManager = gameManager.GetOrCreateManager<CalculateManager>();
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
                case Bullet.SilverStar:
                    obj = bullets[2];
                    break;
                case Bullet.YellowStar:
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