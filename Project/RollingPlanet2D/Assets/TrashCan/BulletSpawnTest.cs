using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnTest : MonoBehaviour
{
    public GameObject bullet;
    public float delayTime = 1.0f;

    void Start()
    {
        StartCoroutine(Spawn(delayTime));
    }

    private IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
