using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArtifact : MonoBehaviour
{
    public float height = .5f;
    public float verticalSpeed = 2f;
    public float rotateSpeed = 45f;
    public CaptureBase captureBase;
    public GameObject gameManager;
    public ArenaManager arenaManager;

    private Vector3 origin;
    private Transform artifactParent;
    private int artifactTeam;
    private string teamName;

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

        gameManager = GameObject.Find("GameManager");

        arenaManager = gameManager.GetComponent<ArenaManager>();
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * verticalSpeed) * height + origin.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotateSpeed);
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.name.Contains("fighter"))
        {
            FighterStatus fighterStatus = collider.gameObject.GetComponent<FighterStatus>();

            if(artifactTeam == fighterStatus.fighterTeam)
            {
                if(transform.parent.name.Contains("teamBase"))
                {
                    arenaManager.SetAiTeammatesReturn(artifactTeam);
                    captureBase.artifactInBase = false;
                    transform.parent = collider.gameObject.transform.GetChild(1);
                    transform.localPosition = new Vector3(0,0,0);
                }
            }
        }
    }

    public void RestoreOrigin()
    {
        captureBase.artifactInBase = true;
        transform.parent = artifactParent;
        transform.position = origin;
    }
}
