using UniRx;
using Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager
{
    public class GameManager : Manager
    {
        public const float TIME_NORMAL = 1.0f;
        public const float TIME_SLOWLY = 0.33f;

        private EffectManager effectManager;
        private Image blackWall;

        private void Awake()
        {
            bool isEasterEgg = GetOrCreateManager<LevelManager>().SetBackground();
            Data.IsEasterEgg = isEasterEgg;
            SpawnRandomPlayer();
        }

        private void Start()
        {
            Data.Score.Total = 0;
            Data.Score.Snow = 0;
            Data.Score.Avoid = 0;

            effectManager = GetOrCreateManager<EffectManager>();
            blackWall = FindComponent<Image>("BlackWall");
        }

        private void SpawnRandomPlayer()
        {
            string playerPath = "Prefabs/Player/";
            int random = Random.Range(0, 2);
            GameObject player = null;
            switch (random)
            {
                case 0:
                    player = Resources.Load($"{playerPath}WinterPlayer") as GameObject;
                    break;
                case 1:
                    player = Resources.Load($"{playerPath}SummerPlayer") as GameObject;
                    break;
            }

            Instantiate(player, Vector3.zero, Quaternion.identity);
        }

        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }

        public void SetTimeScaleToOne(float time)
        {
            StartCoroutine(ESetTimeScaleToOne(time));
        }

        private IEnumerator ESetTimeScaleToOne(float time)
        {
            yield return null;
        }

        public void FadeOut()
        {
            effectManager.FadeOut(blackWall, 1.0f);
        }

        public void FadeIn()
        {
            effectManager.FadeIn(blackWall, 1.0f, (x) =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Restart");
            });
        }
    }
}