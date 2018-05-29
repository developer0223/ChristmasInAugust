using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class RotateTurret : Turret
    {
        public float rotateSpeed = 30.0f;

        new private Transform transform;
        private Transform[] childTransform;
        private Vector3 rotateDirection;

        private void Awake()
        {
            int randomDirection = Random.Range(0, 2);
            float direction = 0;
            switch (randomDirection)
            {
                case 0:
                    direction = -1;
                    break;
                case 1:
                    direction = 1;
                    break;
            }
            rotateDirection = new Vector3(0, 0, direction);

            transform = GetComponent<Transform>();

            int childCount = transform.childCount;

            childTransform = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                childTransform[i] = transform.GetChild(i);
            }
            transform.Rotate(0, 0, Random.Range(0, 360));
            rotateSpeed = Random.Range(20, 31);
        }

        private void Update()
        {
            transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);
        }

        public void RandomShoot(float playTime)
        {
            StartCoroutine(ERandomShoot(playTime));
        }

        protected override IEnumerator EShoot(float playTime, Bullet bullet)
        {
            SetBullet(bullet);
            float cuttentPlayTime = 0.0f;
            while (cuttentPlayTime <= playTime)
            {
                for (int i = 0; i < childTransform.Length; i++)
                {
                    Instantiate(currentBullet, childTransform[i].position, Quaternion.identity);
                }
                cuttentPlayTime += bulletDelayTime;
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(bulletDelayTime);
            }
        }

        /// <summary>
        /// Don't use this
        /// </summary>
        /// <param name="playTime"></param>
        /// <returns></returns>
        private IEnumerator ERandomShoot(float playTime)
        {
            float cuttentPlayTime = 0.0f;
            float randomRange = Random.Range(0, 360);

            while (cuttentPlayTime <= playTime)
            {
                float x = Mathf.Cos(randomRange) * radius;
                float y = Mathf.Sign(randomRange) * radius;

                Vector3 spawnPosition = new Vector3(x, y, 0);

                Instantiate(bullets[0], spawnPosition, Quaternion.identity);

                randomRange += 10;
                if (randomRange > 350)
                {
                    randomRange = 0;
                }
                Debug.Log($"randomRange : {randomRange}");
                cuttentPlayTime += bulletDelayTime;
                yield return new WaitForSeconds(bulletDelayTime);
            }
        }
    }
}
