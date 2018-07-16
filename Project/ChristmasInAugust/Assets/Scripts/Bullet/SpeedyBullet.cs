using UnityEngine;

using Manager;

using static Utility.Data;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpeedyBullet : MonoBehaviour
    {
        #region Property
        protected float Speed { get; set; } = 250.0f;
        protected int Damage { get; set; } = 10;
        #endregion

        #region Cashed Components
        new protected Rigidbody2D rigidbody2D;
        new protected Transform transform;
        protected AudioSource audioSource;
        protected EffectManager effectManager;
        protected ItemManager itemManager;
        protected SoundManager soundManager;
        #endregion

        #region Variables
        // public AudioClip audioClip;

        protected GameManager gameManager;
        protected GameObject targetObject;
        protected Vector2 direction;
        protected string hotBulletParticle = "HotBulletParticle";
        protected string iceBulletParticle = "IceBulletParticle";
        #endregion

        public GameObject particle;
        public AudioClip destroySound;

        void Awake()
        {
            transform = GetComponent<Transform>();
            audioSource = GetComponent<AudioSource>();
            gameManager = GameObject.Find(Tags.GAME_MANAGER).GetComponent<GameManager>();
            effectManager = gameManager.GetOrCreateManager<EffectManager>();
            itemManager = gameManager.GetOrCreateManager<ItemManager>();
            soundManager = gameManager.GetOrCreateManager<SoundManager>();

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            targetObject = GameObject.FindGameObjectWithTag(Tags.PLANET);
            direction = targetObject.transform.position - transform.position;
        }

        private void Start()
        {
            SetSpeedByName();
            rigidbody2D.AddForce(direction.normalized * Speed * GameSpeed);
        }

        public void ShowParticle()
        {
            ParticleManager manager = gameManager.GetOrCreateManager<ParticleManager>();
            manager.Show(particle, transform.position, 2.0f);
        }

        public void PlayDestroySound()
        {
            soundManager.Play(destroySound, transform.position);
        }

        protected void SetSpeedByName()
        {
            string name = gameObject.name;
            switch (name)
            {
                case PrefabName.AngryBullet:
                case PrefabName.SilverSnow:
                case PrefabName.YellowSnow:
                    Speed = 250.0f;
                    break;

                case PrefabName.GrumpyBullet:
                    Speed = 200.0f;
                    break;

                case PrefabName.Watch:
                case PrefabName.Cloud:
                case PrefabName.Heart:
                    Speed = 100.0f;
                    break;
            }
        }

        protected void AddScore()
        {
            if (!gameManager.GetOrCreateManager<ScoreManager>().IsAlive)
            {
                return;
            }
            switch (gameObject.name)
            {
                case PrefabName.AngryBullet:
                    Score.Avoid += 1;
                    break;
                case PrefabName.GrumpyBullet:
                    Score.Avoid += 2;
                    break;
                case PrefabName.SilverSnow:
                    Score.Snow += 3;
                    break;
                case PrefabName.YellowSnow:
                    Score.Snow += 4;
                    break;
                default:
                    Debug.LogWarning($"diffferent tag : {gameObject.name}");
                    break;
            }
        }

        protected void DestroyThis()
        {
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 0;
            renderer.color = color;
            Destroy(gameObject, 3.0f);
        }
    }
}