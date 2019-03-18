using Manager;
using UnityEngine;
using System.Collections;

namespace Enemy
{
    public abstract class Turret : MonoBehaviour
    {
        public GameObject[] bullets = new GameObject[4];

        protected float bulletDelayTime = 0.2f;
        // protected GameObject currentBullet;
        protected float radius = 20.0f;

        protected GameManager gameManager;
        protected CalculateManager calculateManager;

        protected Transform bulletsParent;

        //v
        protected IEnumerator EShootCoroutine;

        public enum Bullet
        {
            AngryBullet,
            GrumpyBullet,
            YellowSnow,
            SilverSnow,
            Random
        }

        private void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            bulletsParent = GameObject.Find("Bullets").GetComponent<Transform>();
            calculateManager = gameManager.GetOrCreateManager<CalculateManager>();
        }

        protected GameObject SetBullet(Bullet bullet)
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
                case Bullet.SilverSnow:
                    obj = bullets[2];
                    break;
                case Bullet.YellowSnow:
                    obj = bullets[3];
                    break;
                case Bullet.Random:
                    obj = bullets[Random.Range(0, bullets.Length)];
                    break;
            }
            return obj;
        }
    }
}