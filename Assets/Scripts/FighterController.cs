using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour
{
    float mainSpeed = 100.0f;
    private float totalRun = 1.0f;
    public GameObject fighter;
    Vector3 currentRotation;
    bool maxLeftRotation = false;
    bool maxRightRotation = false;

    void Start() {
        
    }

    void Update()
    {
        Vector3 velocity = GetBaseInput();
        totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
        velocity = velocity * mainSpeed * Time.deltaTime;

        Vector3 newPosition = transform.position;
        transform.Translate(velocity);
        newPosition.x = transform.position.x;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }

private Vector3 GetBaseInput()
    {
        Vector3 velocity = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(!maxLeftRotation)
            {
                fighter.transform.Rotate(Vector3.down * 3);
            }
            transform.Rotate(new Vector3(0, -3, 0));

            if((fighter.transform.eulerAngles.x * -1) < -300)
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
                fighter.transform.Rotate(Vector3.up * 3);
            }
            transform.Rotate(new Vector3(0, 3, 0));

            if(fighter.transform.eulerAngles.x > 300)
            {
                maxRightRotation = true;
                maxLeftRotation = false;
            }
            else 
            {
                maxRightRotation = false;
            }
        }

        return velocity;
    }
}
