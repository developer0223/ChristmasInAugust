using System;
using UnityEngine;
using System.Collections;

namespace Manager
{
    public class SceneManager : Manager
    {
        public static readonly string LobbyScene = "Lobby";
        public static readonly string MainScene = "Main";

        public void Load(string sceneName)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Load unity scene with sceneName, fadeOutTime, fadeInTime.
        /// </summary>
        public void Load(string sceneName, float fadeOutTime, float fadeInTime)
        {
            // fadeout -> loadscene -> fadein 구조 수정하기 // callback을 너무 더럽게 사용중..ㅠ
            FadeOut(fadeOutTime, (completed) =>
            {
                LoadScene(sceneName, (loaded) =>
                {
                    FadeIn(fadeInTime);
                });
            });
        }

        private void LoadScene(string sceneName, Action<object> loaded)
        {
            // todo : change bgm
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            loaded(null);
        }

        /// <summary>
        /// Fade out with time and callbacks method.
        /// </summary>
        public IEnumerator FadeOut(float time, Action<object> completed)
        {
            #region dummies
            // Debug.Log($"FadeOut started.");
            yield return new WaitForSeconds(time);
            // Debug.Log($"FadeOut completed");
            completed(null);
            #endregion dummies
        }

        /// <summary>
        /// Fade in with time and callbacks method.
        /// </summary>
        public IEnumerator FadeIn(float time)
        {
            #region dummies
            Debug.Log($"FadeIn Started");
            yield return new WaitForSeconds(time);
            Debug.Log($"FadeIn Started");
            #endregion dummies
        }
    }
}