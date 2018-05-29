using UnityEngine;

namespace Manager
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Manager
    {
        private const string BGM_PATH = "Sounds/BGM/";
        private const string SOUND_PATH = "Prefabs/Sound/Sound";
        private AudioSource clipManager;

        public AudioClip age_of_war;

        private void Awake()
        {
            clipManager = GetComponent<AudioSource>();
            clipManager.playOnAwake = false;

            age_of_war = Resources.Load($"{BGM_PATH}MapleStory") as AudioClip;
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

        public void Play(AudioClip audioClip, Vector3 position, float volume = 0.25f)
        {
            if (!audioClip)
                return;
            GameObject soundPrefab = Resources.Load(SOUND_PATH) as GameObject;
            GameObject sound = Instantiate(soundPrefab, position, Quaternion.identity);

            AudioSource source = sound.GetComponent<AudioSource>();
            source.volume = volume;
            source.PlayOneShot(audioClip);
            Destroy(sound, 1.0f);
        }
    }
}
