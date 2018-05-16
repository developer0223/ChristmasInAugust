using UnityEngine;

namespace MainScene.Player
{
    public sealed class SnowMan : Player
    {


        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log($"OnCollisionEnter : {collision.gameObject.name}");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter : {other.gameObject.name}");
        }
    }
}