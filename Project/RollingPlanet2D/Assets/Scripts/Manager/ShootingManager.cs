using Enemy;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Manager
{
    public class ShootingManager : Manager
    {
        RandomTurret randomTurret;
        
        RotateTurret silverTurret;
        RotateTurret pinkTurret;
        RotateTurret angryTurret;
        RotateTurret grumpyTurret;

        private IEnumerator Angry;
        private IEnumerator Grumpy;
        private IEnumerator Yellow;
        private IEnumerator Silver;
        private IEnumerator Mix;

        private float silverStarDelayTime = 15.0f;
        private float pinkStarDelayTime = 50.0f;
        private float mixStarDelayTime = 30.0f;

        private void Awake()
        {
            randomTurret = FindComponent<RandomTurret>("RandomTurret");

            silverTurret = FindComponent<RotateTurret>("SilverTurret");
            pinkTurret = FindComponent<RotateTurret>("PinkTurret");
            angryTurret = FindComponent<RotateTurret>("AngryTurret");
            grumpyTurret = FindComponent<RotateTurret>("GrumpyTurret");
        }

        private void Start()
        {
            Debug.Log("ShootingManager start");
            InitializeCoroutines();
            StartShooting();
        }

        private void InitializeCoroutines()
        {
            Angry = AngryBullet();
            Grumpy = GrumpyBullet();
            Yellow = YellowSnow();
            Silver = SilverSnow();
            Mix = MixSnow();
        }

        void StartShooting()
        {
            StartCoroutine(MixSnow());
            StartCoroutine(SilverSnow());
            StartCoroutine(YellowSnow());
            StartCoroutine(AngryBullet());
            StartCoroutine(GrumpyBullet());
        }

        private IEnumerator SilverSnow()
        {
            while (true)
            {
                silverTurret.Shoot(3.0f, Turret.Bullet.SilverStar);
                yield return new WaitForSeconds(silverStarDelayTime);
            }
        }

        private IEnumerator YellowSnow()
        {
            while (true)
            {
                pinkTurret.Shoot(3.0f, Turret.Bullet.YellowStar);
                yield return new WaitForSeconds(pinkStarDelayTime);
            }
        }

        private IEnumerator MixSnow()
        {
            while (true)
            {
                silverTurret.Shoot(3.0f, Turret.Bullet.SilverStar);
                yield return new WaitForSeconds(0.25f);
                pinkTurret.Shoot(3.0f, Turret.Bullet.YellowStar);
                yield return new WaitForSeconds(mixStarDelayTime);
            }
        }

        private IEnumerator AngryBullet()
        {
            Turret.Bullet angry = Turret.Bullet.AngryBullet;
            while (true)
            {
                // todo : // change below
                yield return new WaitForSeconds(Random.Range(1, 3));
                int random = Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        angryTurret.Shoot(5.0f, angry);
                        break;
                    case 1:
                        randomTurret.Shoot(5.0f, angry);
                        break;
                    case 2:
                        randomTurret.Shoot(10.0f, angry);
                        break;
                    default:
                        Debug.Log("error on shootingmanager.angrybullet()");
                        break;
                }
                // todo : // change below
                yield return new WaitForSeconds(Random.Range(3, 6));
            }
        }

        private IEnumerator GrumpyBullet()
        {
            Turret.Bullet grumpy = Turret.Bullet.GrumpyBullet;
            while (true)
            {
                // todo : // change below
                yield return new WaitForSeconds(Random.Range(3, 6));
                int random = Random.Range(0, 3);
                switch (random)
                {
                    case 0:
                        grumpyTurret.Shoot(5.0f, grumpy);
                        break;
                    case 1:
                        randomTurret.Shoot(5.0f, grumpy);
                        break;
                    case 2:
                        randomTurret.Shoot(10.0f, grumpy);
                        break;
                    default:
                        Debug.Log("error on shootingmanager.angrybullet()");
                        break;
                }
                // todo : // change below
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
        }
    }
}