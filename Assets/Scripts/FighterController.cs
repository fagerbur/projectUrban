using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour {

    public float speed = 100f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;
    private float powerInput;
    private float turnInput;
    private Rigidbody fighterRigidbody;
    public Transform chassis;
    bool maxLeftRotation = false;
    bool maxRightRotation = false;

    void Awake () 
    {
        fighterRigidbody = GetComponent <Rigidbody>();
    }

    void Update () 
    {
        powerInput = Input.GetAxis ("Vertical");
        turnInput = Input.GetAxis ("Horizontal");
        RotateOnTurns();
    }

    void FixedUpdate()
    {
        Ray ray = new Ray (transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
            fighterRigidbody.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }

        fighterRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
        fighterRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
    }

    void RotateOnTurns()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if(!maxLeftRotation)
            {
                chassis.Rotate(0, 0, 3f);
            }
            if(chassis.eulerAngles.z > 30 && chassis.eulerAngles.z < 270)
            {
                maxLeftRotation = true;
                maxRightRotation = false;
            }
            else 
            {
                maxLeftRotation = false;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if(!maxRightRotation)
            {
                chassis.Rotate(0, 0, -3f);
            }
            if(chassis.eulerAngles.z < 330 && chassis.eulerAngles.z > 90)
            {
                maxRightRotation = true;
                maxLeftRotation = false;
            }
            else 
            {
                maxRightRotation = false;   
            }
        }
        if(chassis.eulerAngles.z > 300)
        {
            chassis.Rotate(Vector3.forward * Time.deltaTime * 100);
        }
        if(chassis.eulerAngles.z < 50)
        {
            chassis.Rotate(-Vector3.forward * Time.deltaTime * 100);
        }
    }
}