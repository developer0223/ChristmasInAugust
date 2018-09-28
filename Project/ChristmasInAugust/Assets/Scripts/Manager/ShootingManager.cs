using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Utility;

namespace Manager
{
    public class ShootingManager : Manager
    {
        #region Path
        public const string PATH = "Prefabs/Bullet_Prefabs/";
        public const string Shape = "Shape/";
        public const string Easy = "Easy/";
        public const string Normal = "Normal/";
        public const string Hard = "Hard/";
        #endregion

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

        #region Easy_Name
        public const string CurvedBlue = "Curved_Blue";
        public const string CurvedRed = "Curved_Red";
        public const string CurvedScore = "Curved_Score";
        public const string InazmaBlue = "Inazma_Blue";
        public const string InazmaRed = "Inazma_Red";
        public const string LinearBlue = "Linear_Blue";
        public const string LinearRed = "Linear_Red";
        public const string LinearScore = "Linear_Score";
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

        #region Normal_Name
        public const string CurvedTwoLineBlue = "CurvedTwoLine_Blue";
        public const string CurvedTwoLineMix = "CurvedTwoLine_Mix";
        public const string CurvedTwoLineRed = "CurvedTwoLine_Red";
        public const string CurvedTwoLineScore = "CurvedTwoLine_Score";
        public const string LinearTwoLineBlue = "LinearTwoLine_Blue";
        public const string LinearTwoLineMix = "LinearTwoLine_Mix";
        public const string LinearTwoLineRed = "LinearTwoLine_Red";
        public const string LinearTwoLineScore = "LinearTwoLine_Score";
        #endregion

        #region Hard
        public GameObject Half_Blue;
        public GameObject Half_Mix;
        public GameObject Half_Red;
        public GameObject Half_Score;
        #endregion

        #region Hard_Name
        public const string HalfBlue = "Half_Blue";
        public const string HalfMix = "Half_Mix";
        public const string HalfRed = "Half_Red";
        public const string HalfScore = "Half_Score";
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

        #region Shape_Name
        public const string RoundBlue = "Round_Blue";
        public const string RoundRed = "Round_Red";
        public const string RoundScore = "Round_Score";
        public const string SnowFlowerBullet = "SnowFlower_Bullet";
        public const string SnowFlowerScore = "SnowFlower_Score";
        public const string TreeBullet = "Tree_Bullet";
        public const string TreeScore = "Tree_Score";
        #endregion

        private GameObject player;
        private Transform playerTransform;
        private float PlayerDegree { get { return playerTransform.rotation.z; } }

        private float BulletInterval { get; } = 3.0f;

        private const float Top = 0.0f;
        private const float Right = 90.0f;
        private const float Bottom = 180.0f;
        private const float Left = 270;

        private const float Diagonal = 45.0f;
        private const float RightTop = 45.0f;
        private const float RightBottom = 135.0f;
        private const float LeftButton = 225.0f;
        private const float LeftTop = 315.0f;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Data.Tags.PLAYER);
            playerTransform = player.GetComponent<Transform>();
        }

        private void Start()
        {
            InitializeVariables();
            StartShooting();
        }

        private void InitializeVariables()
        {
            #region Shape
            Round_Blue = Resources.Load($"{PATH}{Shape}{RoundBlue}") as GameObject;
            Round_Red = Resources.Load($"{PATH}{Shape}{RoundRed}") as GameObject;
            Round_Score = Resources.Load($"{PATH}{Shape}{RoundScore}") as GameObject;
            SnowFlower_Bullet = Resources.Load($"{PATH}{Shape}{SnowFlowerBullet}") as GameObject;
            SnowFlower_Score = Resources.Load($"{PATH}{Shape}{SnowFlowerScore}") as GameObject;
            Tree_Bullet = Resources.Load($"{PATH}{Shape}{TreeBullet}") as GameObject;
            Tree_Score = Resources.Load($"{PATH}{Shape}{TreeScore}") as GameObject;
            #endregion

            #region Easy
            Curved_Blue = Resources.Load($"{PATH}{Easy}{CurvedBlue}") as GameObject;
            Curved_Red = Resources.Load($"{PATH}{Easy}{CurvedRed}") as GameObject;
            Curved_Score = Resources.Load($"{PATH}{Easy}{CurvedScore}") as GameObject;
            Inazma_Blue = Resources.Load($"{PATH}{Easy}{InazmaBlue}") as GameObject;
            Inazma_Red = Resources.Load($"{PATH}{Easy}{InazmaRed}") as GameObject;
            Linear_Blue = Resources.Load($"{PATH}{Easy}{LinearBlue}") as GameObject;
            Linear_Red = Resources.Load($"{PATH}{Easy}{LinearRed}") as GameObject;
            Linear_Score = Resources.Load($"{PATH}{Easy}{LinearScore}") as GameObject;
            #endregion

            #region Normal
            CurvedTwoLine_Blue = Resources.Load($"{PATH}{Normal}{CurvedTwoLineBlue}") as GameObject;
            CurvedTwoLine_Mix = Resources.Load($"{PATH}{Normal}{CurvedTwoLineMix}") as GameObject;
            CurvedTwoLine_Red = Resources.Load($"{PATH}{Normal}{CurvedTwoLineRed}") as GameObject;
            CurvedTwoLine_Score = Resources.Load($"{PATH}{Normal}{CurvedTwoLineScore}") as GameObject;
            LinearTwoLine_Blue = Resources.Load($"{PATH}{Normal}{LinearTwoLineBlue}") as GameObject;
            LinearTwoLine_Mix = Resources.Load($"{PATH}{Normal}{LinearTwoLineMix}") as GameObject;
            LinearTwoLine_Red = Resources.Load($"{PATH}{Normal}{LinearTwoLineRed}") as GameObject;
            LinearTwoLine_Score = Resources.Load($"{PATH}{Normal}{LinearTwoLineScore}") as GameObject;
            #endregion

            #region Hard
            Half_Blue = Resources.Load($"{PATH}{Hard}{HalfBlue}") as GameObject;
            Half_Mix = Resources.Load($"{PATH}{Hard}{HalfMix}") as GameObject;
            Half_Red = Resources.Load($"{PATH}{Hard}{HalfRed}") as GameObject;
            Half_Score = Resources.Load($"{PATH}{Hard}{HalfScore}") as GameObject;
            #endregion
        }

        public GameObject SpawnPrefab(GameObject bulletsPrefab, float degree = Top)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, degree));
            GameObject obj = Instantiate(bulletsPrefab, Vector2.zero, rotation);
            Destroy(obj, 10.0f);
            return obj;
        }

        public void StartShooting()
        {
            StartCoroutine(EStartShooting());
        }

        private IEnumerator EStartShooting()
        {
            yield return null;

            StartCoroutine(ENormal((ENormalCallBack) =>
            {
                StartCoroutine(EHard((EHardCallback) =>
                {
                    StartCoroutine(Iterator());
                }));
            }));
            //StartCoroutine(EEasy((EEasyCallBack) =>
            //{
            //    StartCoroutine(ENormal((ENormalCallBack) =>
            //    {
            //        StartCoroutine(EHard((EHardCallback) =>
            //        {
            //            StartCoroutine(Iterator());
            //        }));
            //    }));
            //}));
        }

        private IEnumerator EEasy(Action<object> eEasyCallback)
        {
            yield return null;

            SpawnPrefab(Linear_Red); // 1. 일자탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Linear_Blue, Diagonal); // 2. 일자탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Linear_Score); // 3. 일자탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Curved_Red); // 4. 회오리탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Curved_Blue, Diagonal); // 5. 회오리탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Curved_Score); // 6. 회오리탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Inazma_Red); // 7. 번개탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Inazma_Blue, Diagonal); // 8. 번개탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Score); // 9. 동그라미탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Red, Diagonal); // 10. 동그라미탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Blue); // 11. 동그라미탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Score, Diagonal); // 12. 동그라미탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Tree_Bullet, Top); // 13. 크리스마스트리_탄막
            SpawnPrefab(Tree_Bullet, Right); // 13. 크리스마스트리_탄막
            SpawnPrefab(Tree_Bullet, Bottom); // 13. 크리스마스트리_탄막
            SpawnPrefab(Tree_Bullet, Left); // 13. 크리스마스트리_탄막
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(SnowFlower_Bullet, RightTop); // 14. 눈꽃_탄막
            SpawnPrefab(SnowFlower_Bullet, RightBottom); // 14. 눈꽃_탄막
            SpawnPrefab(SnowFlower_Bullet, LeftButton); // 14. 눈꽃_탄막
            SpawnPrefab(SnowFlower_Bullet, LeftTop); // 14. 눈꽃_탄막
            yield return new WaitForSeconds(BulletInterval);

            eEasyCallback(null);
        }

        private IEnumerator ENormal(Action<object> eNormalCallback)
        {
            yield return null;

            SpawnPrefab(LinearTwoLine_Red); // 1. 일자두줄탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(LinearTwoLine_Blue); // 2. 일자두줄탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(LinearTwoLine_Mix); // 3. 일자두줄탄_혼합
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(CurvedTwoLine_Red); // 4. 회오리두줄탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(CurvedTwoLine_Blue); // 5. 회오리두줄탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(CurvedTwoLine_Mix); // 6. 회오리두줄탄_혼합
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Red); // 7. 동그라미탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Blue, Diagonal); // 8. 동그라미탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Round_Score); // 9. 동그라미탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(LinearTwoLine_Mix); // 10. 일자두줄탄_혼합
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(CurvedTwoLine_Mix); // 11. 회오리두줄탄_혼합
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Tree_Score, PlayerDegree); // 12. 크리스마스탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(SnowFlower_Score, PlayerDegree); // 13. 눈꽃탄_점수
            yield return new WaitForSeconds(BulletInterval);

            eNormalCallback(null);
        }

        private IEnumerator EHard(Action<object> eHardCallback)
        {
            yield return null;

            SpawnPrefab(Half_Red); // 1. 절반덮는탄_빨강
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Half_Blue); // 2. 절반덮는탄_파랑
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Half_Score); // 3. 절반덮는탄_점수
            yield return new WaitForSeconds(BulletInterval);

            SpawnPrefab(Half_Mix); // 4. 절반덮는탄_혼합
            yield return new WaitForSeconds(BulletInterval);

            eHardCallback(null);
        }

        private IEnumerator Iterator()
        {
            while (Data.IsAlive)
            {
                SpawnPrefab(Tree_Score); // 1. 크리스마스트리_점수
                yield return new WaitForSeconds(BulletInterval);

                SpawnPrefab(SnowFlower_Score); // 2. 눈꽃_점수
                yield return new WaitForSeconds(BulletInterval);

                // 3. 중반 13단계 호출
                bool isFinished_Normal = false;
                StartCoroutine(ENormal((ENormalCallback) =>
                {
                    isFinished_Normal = true;
                }));

                while (!isFinished_Normal)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                SpawnPrefab(Tree_Bullet); // 4. 크리스마스트리_탄막
                yield return new WaitForSeconds(BulletInterval);

                SpawnPrefab(SnowFlower_Bullet); // 5. 눈꽃_탄막
                yield return new WaitForSeconds(BulletInterval);

                SpawnPrefab(SnowFlower_Score); // 6. 눈꽃_점수
                yield return new WaitForSeconds(BulletInterval);

                SpawnPrefab(Tree_Score); // 7. 크리스마스트리_점수
                yield return new WaitForSeconds(BulletInterval);

                // 8. 후반 4단계 호출
                bool isFinished_Hard = false;
                StartCoroutine(EHard((EHardCallback) =>
                {
                    isFinished_Hard = true;
                }));

                while (!isFinished_Hard)
                {
                    yield return new WaitForSeconds(0.1f);
                }

                SpawnPrefab(Tree_Bullet); // 9. 크리스마스트리_탄막
                yield return new WaitForSeconds(BulletInterval);

                SpawnPrefab(SnowFlower_Bullet); // 10. 눈꽃_탄막
                yield return new WaitForSeconds(BulletInterval);
            }
            yield return null;
        }
    }
}