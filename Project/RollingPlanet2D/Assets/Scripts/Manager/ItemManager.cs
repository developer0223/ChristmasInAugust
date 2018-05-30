using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Manager
{
    public class ItemManager : Manager
    {
        public GameObject cloudSpawnParticle;
        public GameObject cloudItem;

        CalculateManager calculateManager;

        private readonly string ITEM_PATH = "Prefabs/Item/"; 

        private readonly float spawnHeight = 5.0f;
        private readonly float cloudSpawnDelayTime = 4.0f;

        private void Awake()
        {
            calculateManager = GetOrCreateManager<CalculateManager>();
        }

        private void Start()
        {
            // cloudSpawnParticle = Resources.Load($"{ITEM_PATH}CloudParticle") as GameObject;
            // cloudItem = Resources.Load($"{ITEM_PATH}CloudItem") as GameObject;
        }

        public void MakeCloud()
        {
            StartCoroutine(EMakeCloud());
        }

        private IEnumerator EMakeCloud()
        {
            int randomAngle = Random.Range(0, 360);
            Vector3 position = calculateManager.GetPosition(randomAngle, spawnHeight);
            Quaternion rotation = GameObject.FindGameObjectWithTag(Data.Tags.PLANET).transform.rotation;

            GameObject particle = Instantiate(cloudSpawnParticle, position, rotation);
            yield return new WaitForSeconds(cloudSpawnDelayTime);
            Destroy(particle);
            Instantiate(cloudItem, position, Quaternion.identity);
        }

        public void MakeSlowWatch()
        {
            StartCoroutine(EMakeSlowWatch());
        }

        private IEnumerator EMakeSlowWatch()
        {
            yield return null;
        }
    }
}