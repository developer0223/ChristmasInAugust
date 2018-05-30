using UniRx;
using Utility;
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

        public Image frame;
        public Text snowScore;
        public Text avoidScore;
        public Text currentScore;
        public Text bestScore;

        private const float fadeInTime = 2.0f;
        private const float delayTime = 1.0f;

        private EffectManager effectManager;

        private void Start()
        {
            Debug.Log("start");
            InitializeScore();
            StartCoroutine(FindAndStartFadeIn());
        }

        private void InitializeScore()
        {
            snowScore.text = Data.Score.Snow.ToString();
            avoidScore.text = Data.Score.Avoid.ToString();
            currentScore.text = Data.Score.Total.ToString();
            bestScore.text = PlayerPrefs.GetInt(Data.BestScore.PrefsName, 0).ToString();
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
            effectManager.FadeIn(frame, fadeInTime);

            yield return new WaitForSeconds(delayTime / 2);
            effectManager.FadeIn(snowScore, fadeInTime);

            yield return new WaitForSeconds(delayTime / 2);
            effectManager.FadeIn(avoidScore, fadeInTime);

            yield return new WaitForSeconds(delayTime / 2);
            effectManager.FadeIn(currentScore, fadeInTime);

            yield return new WaitForSeconds(delayTime / 2);
            effectManager.FadeIn(bestScore, fadeInTime);

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
                    effectManager.FadeIn(blackWall, fadeInTime, (x) =>
                    {
                        Data.Score.Total = 0;
                        Data.Score.Snow = 0;
                        Data.Score.Avoid = 0;
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
                    });
                });
        }
    }
}