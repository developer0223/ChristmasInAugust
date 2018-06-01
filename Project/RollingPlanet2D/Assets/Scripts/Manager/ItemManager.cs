using UniRx;
using Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Manager
{
    public class ItemManager : Manager
    {
        private Player.Player player;

        private CalculateManager calculateManager;
        private EffectManager effectManager;
        private GameManager gameManager;

        private readonly string ITEM_PATH = "Prefabs/Item/"; 
        private readonly float spawnHeight = 5.0f;
        private bool isCloudDisplaying = false;

        public GameObject cloudSpawnParticle;
        public GameObject cloudItem;
        private ParticleSystem cloudItemKeepParticle;

        private readonly float cloudDisplayTime = 3.0f;
        private readonly float cloudFadeOutTime = 1.0f;
        private readonly float cloudSpawnDelayTime = 3.0f;

        private GameObject cloud;
        private Image cloudImage;

        private GameObject whiteWall;
        private Image whiteWallImage;

        public GameObject watchSpawnParticle;
        public GameObject watchItem;

        private Button watchButton;

        private readonly float watchDisplayTime = 3.5f;
        private readonly float watchFadeOutTime = 1.5f;
        private readonly float watchSpawnDelayTime = 3.0f;

        private void Awake()
        {
            player = FindComponent<Player.Player>("Snowman");
            watchButton = FindComponent<Button>("WatchButton");
            calculateManager = GetOrCreateManager<CalculateManager>();
            effectManager = GetOrCreateManager<EffectManager>();
            gameManager = GetGameManager();

            cloudItemKeepParticle = FindComponent<ParticleSystem>("CloudItemKeepParticle");
            cloudItemKeepParticle.Stop();
        }

        private void Start()
        {
            // cloudSpawnParticle = Resources.Load($"{ITEM_PATH}CloudParticle") as GameObject;
            // cloudItem = Resources.Load($"{ITEM_PATH}CloudItem") as GameObject;

            InitCloud();
            InitWatch();
        }

        private void InitCloud()
        {
            cloud = GameObject.Find("Cloud");
            cloudImage = cloud.GetComponent<Image>();
            Color color = cloudImage.color;
            color.a = 0;
            cloudImage.color = color;
        }

        private void InitWatch()
        {
            whiteWall = GameObject.Find("WhiteWall");
            whiteWallImage = whiteWall.GetComponent<Image>();
            Color color = whiteWallImage.color;
            color.a = 0;
            whiteWallImage.color = color;

            watchButton.onClick
                .AsObservable()
                .Subscribe((x) =>
                {
                    UseWatchItem();
                });
        }

        // todo : public -> private
        public void UseWatchItem()
        {
            UseWatch();
        }

        public void SpawnCloud(float delayTime)
        {
            StartCoroutine(ESpawnCloud(delayTime));
        }

        public void SpawnSlowWatch(float delayTime)
        {
            StartCoroutine(ESpawnSlowWatch(delayTime));
        }

        private IEnumerator ESpawnCloud(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            int randomAngle = Random.Range(0, 360);
            Vector3 position = calculateManager.GetPosition(randomAngle, spawnHeight);

            GameObject particle = Instantiate(cloudSpawnParticle, position, Quaternion.identity); // rotation
            yield return new WaitForSeconds(cloudSpawnDelayTime);
            Destroy(particle);
            Instantiate(cloudItem, position, Quaternion.identity);
        }

        private IEnumerator ESpawnSlowWatch(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            int randomAngle = Random.Range(0, 360);
            Vector3 position = calculateManager.GetPosition(randomAngle, spawnHeight);

            GameObject particle = Instantiate(watchSpawnParticle, position, Quaternion.identity); // rotation
            yield return new WaitForSeconds(watchSpawnDelayTime);
            Destroy(particle);
            Instantiate(watchItem, position, Quaternion.identity);
        }

        public void UseCloud()
        {
            if (!isCloudDisplaying)
            {
                StartCoroutine(EUseCloud(cloudDisplayTime, cloudFadeOutTime));
            }
        }

        public void UseWatch()
        {
            Debug.Log($"Data.Item.SlowWatch : {Data.Item.SlowWatch}");
            if (Data.Item.SlowWatch >= 1)
            {
                Data.Item.SlowWatch--;
                StartCoroutine(EUseWatch(watchDisplayTime, watchFadeOutTime));
            }
        }

        private IEnumerator EUseCloud(float displayTime, float fadeOutTime)
        {
            isCloudDisplaying = true;
            cloudItemKeepParticle.Play();

            Color color = cloudImage.color;
            color.a = 1;
            cloudImage.color = color;
            cloud.GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(displayTime);
            cloudItemKeepParticle.Stop();
            effectManager.FadeOut(cloudImage, fadeOutTime, (x) =>
            {
                isCloudDisplaying = false;
                cloud.GetComponent<BoxCollider2D>().enabled = false;
            });
        }

        private IEnumerator EUseWatch(float displayTime, float fadeOutTime)
        {
            gameManager.SetTimeScale(0.5f);
            player.Speed *= 2;

            Color color = whiteWallImage.color;
            color.a = 1f;
            whiteWallImage.color = color;
            yield return new WaitForSecondsRealtime(displayTime);
            effectManager.FadeOut(whiteWallImage, fadeOutTime, (x) =>
            {
                gameManager.SetTimeScale(1.0f);
                player.Speed /= 2;
            });
        }
    }
}