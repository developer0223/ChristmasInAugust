using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ParticleManager : Manager
    {

        public void Show(GameObject particlePrefab, Vector3 position, float destroyTime = 1.0f)
        {
            GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);

            Destroy(particle, destroyTime);
        }
    }
}