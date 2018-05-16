using UniRx;
using MyException;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class GameManager : Manager
    {
        public const float TIME_NORMAL = 1.0f;
        public const float TIME_SLOWLY = 0.33f;

        private delegate void EmptyDel();

        new private Transform transform;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            DontDestroyOnLoad(transform);
        }
        /*
            try // GetOrCreateManager
            {
            }
            catch (ComponentNotExistException e) { Debug.Log(e); }
        */

        private void Start()
        {
            // SceneManager sceneManager = GetOrCreateManager<SceneManager>();
            // StartCoroutine(sceneManager.FadeOut(3.0f, completed => { Debug.Log("Completed"); }));
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
    }
}