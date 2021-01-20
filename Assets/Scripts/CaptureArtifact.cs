using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArtifact : MonoBehaviour
{
    float height = .5f;
    float verticalSpeed = 2f;
    float rotateSpeed = 45f;
    Vector3 origin;
    Transform artifactParent;
    int artifactTeam;
    string teamName;

    void Start()
    {
        origin = transform.position;
        artifactParent = transform.parent;
        teamName = transform.parent.name;
        if(teamName.Equals("teamBaseRed"))
        {
            artifactTeam = 0;
        }
        else
        {
            artifactTeam = 1;
        }
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * verticalSpeed) * height + origin.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.name.Contains("fighter"))
        {
            FighterStatus fighterStatus = collider.gameObject.GetComponent<FighterStatus>();
            if(artifactTeam == fighterStatus.fighterTeam)
            {
                transform.parent = collider.gameObject.transform.GetChild(1);
                transform.localPosition = new Vector3(0,0,0);
                fighterStatus.ArtifactCaptured();
            }
            else if (fighterStatus.fighterCapturedArtifact)
            {
                Debug.Log("Score!");
                fighterStatus.ArtifactCaptured();
                collider.transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();
            }
        }
    }

    public void RestoreOrigin()
    {
        transform.parent = artifactParent;
        transform.position = origin;
    }
}
