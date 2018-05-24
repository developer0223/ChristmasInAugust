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

        private void Awake()
        {
            scoreText = FindComponent<Text>("ScoreText");
        }

        private void Start()
        {

        }

        private void Update()
        {
            scoreText.text = Data.Score.Total.ToString();
        }
    }
}