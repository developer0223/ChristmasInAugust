using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour {

    public float radius = 20.0f;
    public GameObject cube;
    public int plusAngle = 5;

	void Start () {

        StartCoroutine(Spawn(180, 360, 10.0f));
	}
	
	void Update () {
		
	}

    public IEnumerator Spawn(int startAngle, int endAngle, float playTime)
    {
        float time = playTime / Mathf.Abs((endAngle - startAngle) / plusAngle);
        Debug.Log(playTime);
        Debug.Log(Mathf.Abs((endAngle - startAngle) * plusAngle));
        Debug.Log(time);
        for (int i = startAngle; i <= endAngle; i+= plusAngle)
        {
            float x = transform.position.x + (Mathf.Cos(i * Mathf.Deg2Rad) * radius);
            float y = transform.position.y + (Mathf.Sin(i * Mathf.Deg2Rad) * radius);
            Debug.Log(i);
            Instantiate(cube, new Vector3(x, y, 0), Quaternion.identity);
            yield return new WaitForSeconds(time);
        }
    }
}
