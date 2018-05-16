using System;
using UnityEngine;

namespace MainScene.Bullet
{
    public class Bell : SpeedyBullet {

        void Start()
        {
            rigidbody2D.AddForce(direction * Speed * Time.deltaTime);
        }
    }
}