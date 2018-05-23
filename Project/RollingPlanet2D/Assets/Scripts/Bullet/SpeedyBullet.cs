using Utility;
using UnityEngine;
using Player;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpeedyBullet : Bullet
    {
        #region Property
        protected float Speed { get; set; } = 1000.0f;
        protected int Damage { get; set; } = 10;
        #endregion

        #region Cashed Components
        new protected Rigidbody2D rigidbody2D;
        new protected Transform transform;
        #endregion

        #region Variables
        protected GameObject targetObject;
        protected Vector2 direction;
        #endregion

        void Awake()
        {
            transform = GetComponent<Transform>();

            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            targetObject = GameObject.FindGameObjectWithTag(Data.Tags.PLANET);
            direction = targetObject.transform.position - transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLAYER))   
            {
                Destroy(gameObject);
                collision.gameObject.GetComponent<SnowMan>().Damage(Damage);
            }
        }
    }
}