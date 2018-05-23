using UnityEngine;

namespace Bullet
{
    public class Bell : SpeedyBullet {

        private void Start()
        {
            Speed = 10000;
            rigidbody2D.AddForce(direction.normalized * Speed * Time.deltaTime);
        }
    }
}