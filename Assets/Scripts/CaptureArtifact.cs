using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArtifact : MonoBehaviour
{
    float height = .5f;
    float verticalSpeed = 2f;
    float rotateSpeed = 45f;
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * verticalSpeed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotateSpeed);
    }
}
