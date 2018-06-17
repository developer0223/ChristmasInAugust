using Enemy;
using Utility;
using UnityEngine;
using System.Collections;

namespace Manager
{
    public class ShootingManager : Manager
    {
        private CalculateManager calculateManager;
        private ItemManager itemManager;

        ThickTurret thickTurret;
        RandomTurret randomTurret;

        RotateTurret angryTurret;
        RotateTurret grumpyTurret;
        RotateTurret silverTurret;
        RotateTurret yellowTurret;

        private readonly float stageOneDelayTIme = 30.0f;
        private readonly float stageTwoDelayTIme = 20.0f;
        private readonly float stageThreeDelayTime = 60.0f;
        private readonly float stageFourDelayTIme = 15.0f;
        private readonly float stageFiveDelayTime = 90.0f;

        private enum Stage
        {
            Stage01, Stage02, Stage03, Stage04, Stage05
        }

        private void Awake()
        {
            calculateManager = GetOrCreateManager<CalculateManager>();
            itemManager = GetOrCreateManager<ItemManager>();

            thickTurret = FindComponent<ThickTurret>("ThickTurret");
            randomTurret = FindComponent<RandomTurret>("RandomTurret");

            angryTurret = FindComponent<RotateTurret>("AngryTurret");
            grumpyTurret = FindComponent<RotateTurret>("GrumpyTurret");
            silverTurret = FindComponent<RotateTurret>("SilverTurret");
            yellowTurret = FindComponent<RotateTurret>("YellowTurret");
        }

        private void Start()
        {
            StartCoroutine(StageManageMent());
        }

        private IEnumerator StageManageMent()
        {
            // stage 1
            Debug.Log("Stage1");
            StartShooting(Stage.Stage01);
            yield return new WaitForSeconds(stageOneDelayTIme);

            // stage 2
            Debug.Log("Stage2");
            StartShooting(Stage.Stage02);
            yield return new WaitForSeconds(stageTwoDelayTIme);

            // stage 3
            Debug.Log("Stage3");
            StartShooting(Stage.Stage03);
            yield return new WaitForSeconds(stageThreeDelayTime);

            // stage 4
            Debug.Log("Stage4");
            StartShooting(Stage.Stage04);
            yield return new WaitForSeconds(stageFourDelayTIme);

            // stage 5
            int hardCount = 1;
            while (true)
            {
                Debug.Log("Stage3 add");
                for (int i = 0; i < hardCount; i++)
                {
                    StartShooting(Stage.Stage05);
                }
                yield return new WaitForSeconds(stageFiveDelayTime);
            }
        }

        void StartShooting(Stage stage)
        {
            switch (stage)
            {
                case Stage.Stage01:
                    StartShootingWrap(stage);
                    break;

                case Stage.Stage02:
                    RotateTurret(stage);
                    break;

                case Stage.Stage03:
                    StartShootingWrap(stage);
                    break;

                case Stage.Stage04:
                    RotateTurret(stage);
                    break;

                case Stage.Stage05:
                    RotateTurret(stage);
                    StartShootingWrap(stage);
                    break;
            }
            
        }

        void StartShootingWrap(Stage stage)
        {
            Item(stage);
            ThickTurret(stage);
            RandomTurret(stage);
        }

        private void ThickTurret(Stage stage)
        {
            float stageTime = 0.0f;

            switch (stage)
            {
                case Stage.Stage01:
                    stageTime = stageOneDelayTIme;
                    break;
                case Stage.Stage03:
                    stageTime = stageThreeDelayTime;
                    break;
                case Stage.Stage05:
                    stageTime = stageFiveDelayTime;
                    break;
            }

            int silverStart = calculateManager.GetRandomAngle();
            int yellowStart = calculateManager.GetRandomAngle();

            int silverEnd = calculateManager.GetRandomEndAngle(silverStart);
            int yellowEnd = calculateManager.GetRandomEndAngle(yellowStart);

            float silverDelayTime = calculateManager.GetRandomDelayTime(0, stageTime / 2);
            float yellowDelayTime = calculateManager.GetRandomDelayTime(0, stageTime / 2);

            float _silverDelayTime = calculateManager.GetRandomDelayTime(stageTime / 2, stageTime);
            float _yellowDelayTime = calculateManager.GetRandomDelayTime(stageTime / 2, stageTime);

            if (Data.IsEasterEgg)
            {
                thickTurret.Shoot(silverStart, silverEnd, Turret.Bullet.AngryBullet, silverDelayTime);
                thickTurret.Shoot(yellowStart, yellowEnd, Turret.Bullet.GrumpyBullet, yellowDelayTime);

                thickTurret.Shoot(silverStart, silverEnd, Turret.Bullet.AngryBullet, _silverDelayTime);
                thickTurret.Shoot(yellowStart, yellowEnd, Turret.Bullet.GrumpyBullet, _yellowDelayTime);
            }
            else // normal play
            {
                thickTurret.Shoot(silverStart, silverEnd, Turret.Bullet.SilverSnow, silverDelayTime);
                thickTurret.Shoot(yellowStart, yellowEnd, Turret.Bullet.YellowSnow, yellowDelayTime);

                thickTurret.Shoot(silverStart, silverEnd, Turret.Bullet.SilverSnow, _silverDelayTime);
                thickTurret.Shoot(yellowStart, yellowEnd, Turret.Bullet.YellowSnow, _yellowDelayTime);
            }
        }

        private void RandomTurret(Stage stage)
        {
            float playTime = 0.0f;
            float delayTime = 0.0f;

            switch (stage)
            {
                case Stage.Stage01:
                    playTime = stageOneDelayTIme;
                    delayTime = 0.5f;
                    break;

                case Stage.Stage02:
                    DoNothing();
                    break;

                case Stage.Stage03:
                    playTime = stageThreeDelayTime;
                    delayTime = 0.35f;
                    break;

                case Stage.Stage04:
                    DoNothing();
                    break;

                case Stage.Stage05:
                    playTime = stageFiveDelayTime;
                    delayTime = 0.2f;
                    break;
            }

            if (Data.IsEasterEgg)
            {
                randomTurret.Shoot(playTime, delayTime * 2, Turret.Bullet.AngryBullet);
                randomTurret.Shoot(playTime, delayTime * 2, Turret.Bullet.GrumpyBullet);

                randomTurret.Shoot(playTime, delayTime, Turret.Bullet.SilverSnow);
                randomTurret.Shoot(playTime, delayTime, Turret.Bullet.YellowSnow);
            }
            else
            {
                randomTurret.Shoot(playTime, delayTime, Turret.Bullet.AngryBullet);
                randomTurret.Shoot(playTime, delayTime, Turret.Bullet.GrumpyBullet);

                randomTurret.Shoot(playTime, delayTime * 2, Turret.Bullet.SilverSnow);
                randomTurret.Shoot(playTime, delayTime * 2, Turret.Bullet.YellowSnow);
            }
        }

        private void RotateTurret(Stage stage)
        {
            float playTime = 0.0f; // = duration
            float stageTime = 0.0f;
            float delayTime = 0.0f;

            switch (stage)
            {
                case Stage.Stage01:
                    DoNothing();
                    return;

                case Stage.Stage02:
                    playTime = 4.0f;
                    stageTime = stageTwoDelayTIme;
                    delayTime = 4.0f;
                    break;

                case Stage.Stage03:
                    playTime = Random.Range(1.0f, 2.0f);
                    stageTime = stageThreeDelayTime;
                    delayTime = 10.0f;
                    break;

                case Stage.Stage04:
                    playTime = 3.0f;
                    stageTime = stageFourDelayTIme;
                    delayTime = 3.0f;
                    break;

                case Stage.Stage05:
                    playTime = Random.Range(1.5f, 3.0f);
                    stageTime = stageFiveDelayTime;
                    delayTime = 15.0f;
                    break;
            }

            int randomTurret;
            RotateTurret turret;
            for (float i = 0.0f; i < stageTime; i+= delayTime)
            {
                randomTurret = Random.Range(0, 2);

                if (Data.IsEasterEgg)
                {
                    turret = (randomTurret == 1 ? silverTurret : yellowTurret);
                }
                else
                {
                    turret = (randomTurret == 1 ? angryTurret : grumpyTurret);
                }
                
                float _delay = calculateManager.GetRandomDelayTime(i, i + delayTime);
                turret.Shoot(playTime, _delay);
            }
        }

        private void Item(Stage stage)
        {
            float stageTime = GetStageDelayTime(stage);
            float cloudDelayTime = 0.0f;
            float watchDelayTime = 0.0f;

            switch (stage)
            {
                case Stage.Stage01:
                    cloudDelayTime = 15.0f;
                    watchDelayTime = 30.0f;
                    break;

                case Stage.Stage02:
                    cloudDelayTime = 20.0f;
                    watchDelayTime = 10.0f;
                    break;

                case Stage.Stage03:
                    cloudDelayTime = 10.0f;
                    watchDelayTime = 20.0f;
                    break;

                case Stage.Stage04:
                    cloudDelayTime = 7.5f;
                    watchDelayTime = 5.0f;
                    break;

                case Stage.Stage05:
                    cloudDelayTime = 8.0f;
                    watchDelayTime = 15.0f;
                    break;
            }

            for (float i = 0.0f; i < stageTime; i += cloudDelayTime)
            {
                float _delay = calculateManager.GetRandomDelayTime(i, i + cloudDelayTime);
                itemManager.SpawnCloud(_delay);
                // itemManager.SpawnCloud(_delay);
            }

            for (float i = 0.0f; i < stageTime; i += watchDelayTime)
            {
                float _delay = calculateManager.GetRandomDelayTime(i, i + watchDelayTime);
                itemManager.SpawnSlowWatch(_delay);
                // itemManager.SpawnSlowWatch(_delay);
            }
        }

        // no useage
        private float GetStageDelayTime(Stage stage)
        {
            float result = 0.0f;
            switch (stage)
            {
                case Stage.Stage01:
                    result = stageOneDelayTIme;
                    break;
                case Stage.Stage03:
                    result = stageThreeDelayTime;
                    break;
                case Stage.Stage05:
                    result = stageFiveDelayTime;
                    break;
            }
            return result;
        }
    }
}