using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy
{
    public class RandomTurret : Turret
    {
        void Start()
        {
            bulletDelayTime = 0.1f;
            SetBullet(Bullet.AngryBullet);
        }

        protected override IEnumerator EShoot(float playTime, Bullet bullet)
        {
            float currentPlayTime = 0.0f;
            while (currentPlayTime < playTime)
            {
                float randomAngle = Random.Range(0, 360);
                Vector3 spawnPosition = GetPosition(randomAngle);

                Instantiate(currentBullet, spawnPosition, Quaternion.identity);

                currentPlayTime += bulletDelayTime;

                yield return new WaitForSeconds(bulletDelayTime);
            }
            
        }
    }
}