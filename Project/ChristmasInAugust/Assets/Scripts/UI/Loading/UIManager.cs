using UniRx;
using Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager.Lobby
{
    public class UIManager : Manager
    {
        public Image playButton;
        public Image playText;
        public Image tutorialButton;
        public Text tutorialText;
        public Canvas toturialPopUp;
        public Image blackWall;

        private bool isPopUpDisplaying = false;

        private readonly float fadeInTime = 2.0f;
        private readonly float delayTime = 1.0f;

        private EffectManager effectManager;

        private void Awake()
        {
            effectManager = GameObject.Find("GameManager").GetComponent<EffectManager>();
        }

        private void Start()
        {
            StartCoroutine(StartFadeIn());
            SetOnClickListener();
        }

        private IEnumerator StartFadeIn()
        {
            yield return new WaitForSeconds(delayTime);
            effectManager.FadeIn(playText, fadeInTime, (x) => DoNothing());

            yield return new WaitForSeconds(1.0f);

            effectManager.FadeIn(tutorialButton, fadeInTime, (x) => DoNothing());
            effectManager.FadeIn(tutorialText, fadeInTime);

            yield return new WaitForSeconds(1.0f);

            effectManager.FadeIn(playButton, fadeInTime, (x) => DoNothing());
        }

        private void SetOnClickListener()
        {
            Button playBtn = playButton.gameObject.GetComponent<Button>();
            playBtn.onClick
                .AsObservable()
                .Subscribe(
                (x) =>
                {
                    effectManager.FadeIn(blackWall, fadeInTime, (xx) =>
                    {
                        Data.InitData();
                        SceneManager.Load(SceneManager.MainScene);
                    });
                });

            Button tutorialBtn = tutorialButton.gameObject.GetComponent<Button>();
            tutorialBtn.onClick
                .AsObservable()
                .Subscribe(
                (x) =>
                {
                    isPopUpDisplaying = !isPopUpDisplaying;
                    toturialPopUp.gameObject.SetActive(isPopUpDisplaying);
                });
        }

    }
}