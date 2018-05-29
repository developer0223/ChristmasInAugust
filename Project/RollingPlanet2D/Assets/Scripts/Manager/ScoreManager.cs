using Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Manager
{
    public class ScoreManager : Manager
    {
        private Text scoreText;
        private Text bestScoreText;

        public bool IsAlive { get; set; }

        private void Awake()
        {
            scoreText = FindComponent<Text>("ScoreText");
            bestScoreText = FindComponent<Text>("BestScoreText");
        }

        private void Start()
        {
            IsAlive = true;
            RefreshBestScore();
        }

        private void Update()
        {
            if (IsAlive)
            {
                Data.Score.Total = Data.Score.Snow + Data.Score.Avoid;
                scoreText.text = Data.Score.Total.ToString();
            }
        }

        public void RenewalScore()
        {
            if (Data.Score.Total > PlayerPrefs.GetInt(Data.BestScore.PrefsName))
            {
                RenewalBestScore();
            }
        }

        private void RenewalBestScore()
        {
            PlayerPrefs.SetInt(Data.BestScore.PrefsName, Data.Score.Total);
            PlayerPrefs.Save();
        }

        public void RefreshBestScore()
        {
            bestScoreText.text = PlayerPrefs.GetInt(Data.BestScore.PrefsName).ToString();
        }
    }
}