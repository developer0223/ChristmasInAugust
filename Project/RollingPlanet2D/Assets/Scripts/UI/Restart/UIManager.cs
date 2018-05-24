using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager.Restart
{
    public class UIManager : Manager
    {
        [SerializeField] private Image playButton;
        [SerializeField] private Image playText;

        private const float fadeInTime = 2.0f;
        private const float delayTime = 1.0f;

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
            effectManager.FadeIn(playText, fadeInTime, (x) => Debug.Log("Restart.PlayText fadeIn complete."));
            yield return new WaitForSeconds(delayTime);
            effectManager.FadeIn(playButton, fadeInTime, (x) => Debug.Log("Restart.PlayButton fadeIn complete."));
        }

        private void SetOnClickListener()
        {
            Button button = playButton.gameObject.GetComponent<Button>();
            button.onClick
                .AsObservable()
                .Subscribe(_ =>
                   UnityEngine.SceneManagement.SceneManager.LoadScene("Main")
                );
        }
    }
}