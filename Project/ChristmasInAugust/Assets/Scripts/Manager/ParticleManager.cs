using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ParticleManager : Manager
    {
        private Transform particleParent;

        private void Awake()
        {
            particleParent = GameObject.Find("Particles").GetComponent<Transform>();
        }

        public void Show(GameObject particlePrefab, Vector3 position, float destroyTime = 1.0f)
        {
            GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity, particleParent);

            Destroy(particle, destroyTime);
        }
    }
}