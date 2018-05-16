using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Manager
    {
        private const string BGM_PATH = "Sounds/BGM/";
        private AudioSource clipManager;

        public AudioClip age_of_war;

        private void Awake()
        {
            clipManager = GetComponent<AudioSource>();
            clipManager.playOnAwake = false;

            age_of_war = Resources.Load($"{BGM_PATH}age_of_war") as AudioClip;
        }

        private void Start()
        {
            PlayBGM(age_of_war);
        }

        public void PlayBGM(AudioClip clip)
        {
            clipManager.clip = clip;
            clipManager.Play();
        }
    }
}
