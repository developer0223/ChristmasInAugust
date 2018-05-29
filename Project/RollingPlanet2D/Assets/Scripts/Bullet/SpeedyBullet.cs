using Utility;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpeedyBullet : Bullet
    {
        #region Property
        protected float Speed { get; set; } = 250.0f;
        protected int Damage { get; set; } = 10;
        #endregion

        #region Cashed Components
        new protected Rigidbody2D rigidbody2D;
        new protected Transform transform;
        protected AudioSource audioSource;
        #endregion

        #region Variables
        public AudioClip audioClip;

        protected GameObject targetObject;
        protected Vector2 direction;
        protected string hotBulletParticle = "HotBulletParticle";
        protected string iceBulletParticle = "IceBulletParticle";
        #endregion

        public GameObject particle;

        void Awake()
        {
            transform = GetComponent<Transform>();
            audioSource = GetComponent<AudioSource>();

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            targetObject = GameObject.FindGameObjectWithTag(Data.Tags.PLANET);
            direction = targetObject.transform.position - transform.position;
        }

        public void ShowParticle()
        {
            GameObject particle = Instantiate(this.particle, transform.position, Quaternion.identity);
            Destroy(particle, 1.0f);
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