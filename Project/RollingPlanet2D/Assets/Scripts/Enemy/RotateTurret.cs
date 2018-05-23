using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class RotateTurret : Enemy
    {
        public float rotateSpeed = 30.0f;
        public GameObject[] bullet;

        new private Transform transform;
        private Transform[] childTransform;

        private Vector3 direction = new Vector3(0, 0, -1);

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Start()
        {
            int childCount = transform.childCount;

            childTransform = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                childTransform[i] = transform.GetChild(i);
            }

            // test
            Invoke("Test", 5f);
        }

        private void Test()
        {
            Shoot(10.0f, 0.1f);
        }

        private void Update()
        {
            transform.Rotate(direction * rotateSpeed * Time.deltaTime);
        }

        public void Shoot(float time, float bulletDelayTime)
        {
            StartCoroutine(EShoot(time, bulletDelayTime));
        }

        private IEnumerator EShoot(float playTime, float bulletDelayTime)
        {
            float cuttentPlayTime = 0.0f;
            while (cuttentPlayTime <= playTime)
            {
                for (int i = 0; i < childTransform.Length; i++)
                {
                    Instantiate(bullet[0], childTransform[i].position, Quaternion.identity);
                }
                cuttentPlayTime += bulletDelayTime;
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(bulletDelayTime);
            }
        }
    }
}
