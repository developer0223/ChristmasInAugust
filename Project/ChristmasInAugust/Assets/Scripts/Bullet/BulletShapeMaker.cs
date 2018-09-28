using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Utility;

namespace Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletShapeMaker : MonoBehaviour
    {
        protected float Speed { get; set; } = 200.0f;

        new private Rigidbody2D rigidbody2D;
        private Vector2 direction;
        private GameObject targetObject;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.simulated = true;
            rigidbody2D.gravityScale = 0;

            targetObject = GameObject.FindGameObjectWithTag(Data.Tags.PLANET);
            direction = targetObject.transform.position - transform.position;
        }

        private void Start()
        {
            rigidbody2D.AddForce(direction.normalized * Speed * Data.GameSpeed);
        }
    }
}