using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //TODO: Will need to address collision in here
    public GameObject fighter;
    private Vector3 offsetPosition;
    private Quaternion offsetRotation;

    void Start ()
    {
        offsetPosition = transform.position - fighter.transform.position;
        offsetRotation = fighter.transform.rotation;
    }

    void Update ()
    {
        transform.position = fighter.transform.position + offsetPosition;
        transform.Rotate(new Vector3(offsetRotation.x, 0, offsetRotation.z));
    }
}