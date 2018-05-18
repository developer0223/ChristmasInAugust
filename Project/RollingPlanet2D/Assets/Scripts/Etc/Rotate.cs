using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotateSpeed = 30.0f;
    new private Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime);
    }
}
