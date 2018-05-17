using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnTest : MonoBehaviour
{
    public GameObject[] bullets;
    public float delayTime = 0.5f;

    void Start()
    {
        StartCoroutine(Spawn(delayTime));
    }

    private IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            Instantiate(bullets[Random.Range(0, 2)], transform.position, transform.rotation);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
