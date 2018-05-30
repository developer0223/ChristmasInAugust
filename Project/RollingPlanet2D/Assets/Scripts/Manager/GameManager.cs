using UniRx;
using Utility;
using MyException;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager
{
    public class GameManager : Manager
    {
        public const float TIME_NORMAL = 1.0f;
        public const float TIME_SLOWLY = 0.33f;

        private delegate void EmptyDel();

        private EffectManager effectManager;
        private Image blackWall;

        private bool isCloudDisplaying = false;

        private const float cloudDisplayTime = 3.0f;
        private const float cloudFadeOutTime = 1.0f;

        private GameObject cloud;
        private Image cloudImage;

        private void Start()
        {
            Data.Score.Total = 0;
            Data.Score.Snow = 0;
            Data.Score.Avoid = 0;

            effectManager = GetOrCreateManager<EffectManager>();
            blackWall = FindComponent<Image>("BlackWall");

            InitCloud();
        }

        public void SetTimeScale(float scale)
        {
            Debug.Log($"Set Time.timeScale to {scale}.");
            Time.timeScale = scale;
        }

        public T GetManager<T>() where T : Manager
        {
            return GetOrCreateManager<T>();
        }

        public void FadeOut()
        {
            effectManager.FadeOut(blackWall, 2.0f, (x) =>
            {
                // do nothing
            });
        }

        public void FadeIn()
        {
            Debug.Log("fadeIn");
            effectManager.FadeIn(blackWall, 2.0f, (x) =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Restart");
                Debug.Log("restart");
            });
            Debug.Log("fadein complete");
        }

        public void MakeCloud()
        {
            if (!isCloudDisplaying)
            {
                StartCoroutine(EMakeCloud(cloudDisplayTime, cloudFadeOutTime));
            }
        }

        private void InitCloud()
        {
            cloud = GameObject.Find("Cloud");
            cloudImage = cloud.GetComponent<Image>();
            Color color = cloudImage.color;
            color.a = 0;
            cloudImage.color = color;
        }

        private IEnumerator EMakeCloud(float displayTime, float fadeOutTime)
        {
            isCloudDisplaying = true;
            Color color = cloudImage.color;
            color.a = 1;
            cloudImage.color = color;
            cloud.GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(displayTime);
            effectManager.FadeOut(cloudImage, fadeOutTime / 2,
                (x) =>
                {
                    isCloudDisplaying = false;
                    cloud.GetComponent<BoxCollider2D>().enabled = false;
                });
        }
    }
}