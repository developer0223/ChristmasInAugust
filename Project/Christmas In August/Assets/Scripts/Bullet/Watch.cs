using Utility;
using UnityEngine;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Watch : SpeedyBullet
    {
        //new private void Awake()
        //{
        //    base.Awake();

        //    Speed = 10.0f;

        //    targetObject = GameObject.FindGameObjectWithTag(Data.Tags.PLANET);
        //    direction = targetObject.transform.position - transform.position;

        //    rigidbody2D = GetComponent<Rigidbody2D>();
        //    rigidbody2D.AddForce(direction * Speed);
        //}

        new private void Awake()
        {
            base.Awake();

            BulletShapeMaker maker = GetComponent<BulletShapeMaker>();
            maker.speed = 100.0f;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Data.Tags.PLAYER) ||
                collision.CompareTag(Data.Tags.CLOUD))
            {
                Data.Item.SlowWatch++;
                Destroy(gameObject);
            }
            else if (collision.CompareTag(Data.Tags.PLANET))
            {
                Destroy(gameObject);
            }
        }
    }
}