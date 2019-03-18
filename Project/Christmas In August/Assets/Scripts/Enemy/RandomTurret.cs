using Manager;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy
{
    public class RandomTurret : Turret
    {
        void Start()
        {
            bulletDelayTime = 0.3f;
            SetBullet(Bullet.AngryBullet);
        }

        public void Shoot(float playTime, float delayTime, Bullet bullet)
        {
            StartCoroutine(EShoot(playTime, delayTime, bullet));
        }

        protected IEnumerator EShoot(float playTime, float delayTime, Bullet bullet)
        {
            GameObject currentBullet = SetBullet(bullet);

            float currentPlayTime = 0.0f;
            while (currentPlayTime < playTime)
            {
                float randomAngle = Random.Range(0, 360);
                Vector3 spawnPosition = calculateManager.GetPosition(randomAngle, radius);

                Instantiate(currentBullet, spawnPosition, Quaternion.identity, bulletsParent);

                currentPlayTime += bulletDelayTime;

                yield return new WaitForSeconds(bulletDelayTime);
            }
            
        }
    }
}