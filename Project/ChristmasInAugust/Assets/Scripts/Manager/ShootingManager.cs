using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Utility;

namespace Manager
{
    public class ShootingManager : Manager
    {
        #region Easy
        public GameObject Curved_Blue;
        public GameObject Curved_Red;
        public GameObject Curved_Score;
        public GameObject Inazma_Blue;
        public GameObject Inazma_Red;
        public GameObject Linear_Blue;
        public GameObject Linear_Red;
        public GameObject Linear_Score;
        #endregion

        #region Normal
        public GameObject CurvedTwoLine_Blue;
        public GameObject CurvedTwoLine_Mix;
        public GameObject CurvedTwoLine_Red;
        public GameObject CurvedTwoLine_Score;
        public GameObject LinearTwoLine_Blue;
        public GameObject LinearTwoLine_Mix;
        public GameObject LinearTwoLine_Red;
        public GameObject LinearTwoLine_Score;
        #endregion

        #region Hard
        public GameObject Half_Blue;
        public GameObject Half_Mix;
        public GameObject Half_Red;
        public GameObject Half_Score;
        #endregion

        #region Shape
        public GameObject Round_Blue;
        public GameObject Round_Red;
        public GameObject Round_Score;
        public GameObject SnowFlower_Bullet;
        public GameObject SnowFlower_Score;
        public GameObject Tree_Bullet;
        public GameObject Tree_Score;
        #endregion

        private float BulletInterval { get; } = 2.0f;

        private void Start()
        {
            StartShooting();
        }

        public GameObject SpawnPrefab(GameObject bulletsPrefab)
        {
            GameObject obj = Instantiate(bulletsPrefab, Vector2.zero, Quaternion.identity);
            return obj;
        }

        public void StartShooting()
        {
            StartCoroutine(EStartShooting());
        }

        private IEnumerator EStartShooting()
        {
            yield return null;

            StartCoroutine(EEasy());
            //StartCoroutine(ENormal());
            //StartCoroutine(EHard());

            //while (Data.IsAlive)
            //{

            //}
        }

        private IEnumerator EEasy()
        {
            yield return null;

            SpawnPrefab(Linear_Red); // 일자탄_빨강
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Linear_Blue); // 일자탄_파랑
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Linear_Score); // 일자탄_점수
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Curved_Red); // 회오리탄_빨강
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Curved_Blue); // 회오리탄_파랑
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Curved_Score); // 회오리탄_점수
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Inazma_Red); // 번개탄_빨강
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Inazma_Blue); // 번개탄_파랑
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Round_Score); // 동그라미탄_점수
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Round_Red); // 동그라미탄_빨강
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Round_Blue); // 동그라미탄_파랑
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Round_Score); // 동그라미탄_점수
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(Tree_Bullet); // 크리스마스트리_탄막
            yield return new WaitForSecondsRealtime(BulletInterval);

            SpawnPrefab(SnowFlower_Bullet); // 눈꽃_탄막
            yield return new WaitForSecondsRealtime(BulletInterval);
        }

        private IEnumerator ENormal()
        {
            yield return null;
            yield return new WaitForSecondsRealtime(BulletInterval);

        }

        private IEnumerator EHard()
        {
            yield return null;
            yield return new WaitForSecondsRealtime(BulletInterval);

        }
    }
}