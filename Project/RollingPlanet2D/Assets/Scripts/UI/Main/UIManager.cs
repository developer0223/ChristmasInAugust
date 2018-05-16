using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Manager
{
    public class UIManager : Manager
    {
        private bool IsPaused { get; set; } = false;

        private GameManager gameManager;

        private Button pausedButton;
        private List<Canvas> canvasList;

        #region pause state
        private Canvas pauseCanvas;
        private Button resumeButton;
        private Button settingsButton;
        private Button quitButton;
        #endregion

        #region settings state
        private Canvas settingsCanvas;
        private Button muteBGMButton;
        private Button muteEffectButton;
        private Button saveButton;
        private Button saveAndResumeButton;
        #endregion

        void Start()
        {
            FindManager();
            FindCanvas();
            FindButton();
        }

        private void FindButton()
        {
            #region paused state
            pausedButton = FindComponent<Button>("PauseButton");
            resumeButton = FindComponent<Button>("ResumeButton");
            settingsButton = FindComponent<Button>("SettingsButton");
            quitButton = FindComponent<Button>("QuitButton");
            #endregion
            #region settings state
            muteBGMButton = FindComponent<Button>("MuteBGMButton");
            muteEffectButton = FindComponent<Button>("MuteEffectButton");
            saveButton = FindComponent<Button>("SaveButton");
            saveAndResumeButton = FindComponent<Button>("SaveAndResumeButton");
            #endregion

            SetOnClickListener();
        }

        private void FindManager()
        {
            gameManager = FindComponent<GameManager>("GameManager");
        }

        private void FindCanvas()
        {
            pauseCanvas = FindComponent<Canvas>("PausedCanvas");
            settingsCanvas = FindComponent<Canvas>("SettingsCanvas");

            canvasList = SaveCanvas(pauseCanvas, settingsCanvas);
        }

        public void SetOnClickListener()
        {
            #region pausebutton
            pausedButton.onClick
                .AsObservable()
                .Subscribe(_ =>
                {
                    if (IsPaused)
                    {
                        Resume();
                    }
                    else
                    {
                        Pause();
                    }
                });
            #endregion
            #region resumeButton
            resumeButton.onClick
                .AsObservable()
                .Subscribe(_ =>
                {
                    Resume();
                });
            #endregion
            #region settingsButton
            settingsButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    List<object> settingsList;
                    // todo : fix KeyNotFoundException error.
                    gameManager.GetOrCreateManager<SettingsManager>().Load(out settingsList);
                    // todo : do somthing with settingsList
                    ShowCanvas(settingsCanvas);
                });
            #endregion
            #region quitButton
            quitButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    Debug.Log($"quitButton clicked");
                });
            #endregion
            #region muteBGMButton
            muteBGMButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    // mute or play sound
                    Debug.Log($"muteBGMButton clicked.");
                });
            #endregion
            #region muteEffectButton
            muteEffectButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    Debug.Log($"muteEffectButton clicked.");
                    // mute or play sound
                });
            #endregion
            #region saveButton
            saveButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    GetOrCreateManager<SettingsManager>().Save(true, false);
                    ShowCanvas(pauseCanvas);
                });
            #endregion
            #region saveAndResumeButton
            saveAndResumeButton.onClick
                .AsObservable()
                .Subscribe(x =>
                {
                    ShowCanvas(null);
                });
            #endregion
        }

        private void Pause()
        {
            gameManager.SetTimeScale(0);
            pauseCanvas.enabled = true;
            IsPaused = !IsPaused;
        }

        private void Resume()
        {
            gameManager.SetTimeScale(1);
            pauseCanvas.enabled = false;
            IsPaused = !IsPaused;
        }

        private List<Canvas> SaveCanvas(params Canvas[] canvases)
        {
            List<Canvas> list = new List<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                list.Add(canvas);
            }
            return list;
        }

        private void ShowCanvas(Canvas upCanvas)
        {
            foreach (Canvas canvas in canvasList)
            {
                canvas.enabled = false;
            }
            if (upCanvas)
            {
                upCanvas.enabled = true;
            }
            else
            {
                Resume();
            }
        }
    }
}