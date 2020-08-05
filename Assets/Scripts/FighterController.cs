using UnityEngine;
using System.Collections;

public class FighterController : MonoBehaviour
{
    float mainSpeed = 100.0f;
    private float totalRun = 1.0f;
    public Transform fighter;
    bool maxLeftRotation = false;
    bool maxRightRotation = false;

    void Start() {
    }

    void FixedUpdate()
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
            transform.Rotate(new Vector3(0, -3f, 0));

            if(!maxLeftRotation)
            {
                fighter.Rotate(0, 0, 3f);
            }
            if(fighter.eulerAngles.z > 30 && fighter.eulerAngles.z < 270)
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
            transform.Rotate(new Vector3(0, 3f, 0));

            if(!maxRightRotation)
            {
                fighter.Rotate(0, 0, -3f);
            }
            if(fighter.eulerAngles.z < 330 && fighter.eulerAngles.z > 90)
            {
                maxRightRotation = true;
                maxLeftRotation = false;
            }
            else 
            {
                maxRightRotation = false;
            }
        }
        if(fighter.eulerAngles.z > 300)
        {
            fighter.Rotate(Vector3.forward * Time.deltaTime * 100);
        }
        if(fighter.eulerAngles.z < 50)
        {
            fighter.Rotate(-Vector3.forward * Time.deltaTime * 100);
        }

        return velocity;
    }
}
