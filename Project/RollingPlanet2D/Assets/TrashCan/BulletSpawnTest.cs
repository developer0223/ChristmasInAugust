using Utility;
using UnityEngine;
using System.Collections;

public class BulletSpawnTest : MonoBehaviour
{
    public GameObject[] bullets;
    private float delayTime = 0.2f;

    void Start()
    {
        StartCoroutine(Spawn(delayTime));
    }

    private IEnumerator Spawn(float delayTime)
    {
        while (true)
        {
            Instantiate(bullets[Random.Range(0, bullets.Length)], transform.position, transform.rotation);
            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(delayTime);
        }
    }
}
