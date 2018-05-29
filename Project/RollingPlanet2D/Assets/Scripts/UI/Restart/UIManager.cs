using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager.Restart
{
    public class UIManager : Manager
    {
        public Image restartButton;
        public Image restartText;

        public Image blackWall;

        private const float fadeInTime = 2.0f;
        private const float delayTime = 1.0f;

        private EffectManager effectManager;

        private void Start()
        {
            Debug.Log("start");
            StartCoroutine(FindAndStartFadeIn());
        }

        private IEnumerator FindAndStartFadeIn()
        {
            effectManager = FindComponent<EffectManager>("GameManager");
            // GameObject.Find("GameManager").GetComponent<EffectManager>();
            // restartText = FindComponent<Image>("DieMessage");
            // restartButton = FindComponent<Image>("RestartButton");

            SetOnClickListener();
            StartCoroutine(StartFadeIn());
            yield return null;
        }

        private IEnumerator StartFadeIn()
        {
            yield return new WaitForSeconds(delayTime);
            effectManager.FadeIn(restartText, fadeInTime);
            yield return new WaitForSeconds(delayTime);
            effectManager.FadeIn(restartButton, fadeInTime);
        }

        private void SetOnClickListener()
        {
            Button button = restartButton.gameObject.GetComponent<Button>();
            button.onClick
                .AsObservable()
                .Subscribe(
                _ =>
                {
                    effectManager.FadeOut(blackWall, fadeInTime, (x) =>
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                    });
                });
        }
    }
}