using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureArtifact : MonoBehaviour
{
    public float height = .5f;
    public float verticalSpeed = 2f;
    public float rotateSpeed = 45f;
    private Vector3 origin;
    private Transform artifactParent;
    private int artifactTeam;
    private string teamName;
    public GameObject gameManager;
    public ArenaManager arenaManager;
    public ArenaManager_AgentTrainer agentArenaManager;

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

        // if(GameManager.TryGetComponent<ArenaManager>(out var arenaFake))
        // {
        //     arenaManager = GameManager.GetComponent<ArenaManager>();
        // }
        // else
        // {
            agentArenaManager = gameManager.GetComponent<ArenaManager_AgentTrainer>();
        // }
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
            FighterAgent fighterAgent = collider.gameObject.GetComponent<FighterAgent>();

            AgentStatus fighterStatus = collider.gameObject.GetComponent<AgentStatus>();
            // FighterStatus fighterStatus = collider.gameObject.GetComponent<FighterStatus>();
            if(artifactTeam == fighterStatus.fighterTeam)
            {
                if(transform.parent.name.Contains("teamBase"))
                {
                    fighterAgent.AgentCapturedArtifact();
                    fighterAgent.MatchEnded();
                }
                // transform.parent = collider.gameObject.transform.GetChild(1);
                // transform.localPosition = new Vector3(0,0,0);
                // fighterStatus.ArtifactCaptured();

            }
            else if (fighterStatus.fighterCapturedArtifact)
            {
                fighterAgent.AgentReturnedArtifact();

                Debug.Log("Score!");

                if(artifactTeam == 0)
                {
                    agentArenaManager.teamRedScore++;
                    print("teamRedScore = " + agentArenaManager.teamRedScore);
                }
                else
                {
                    agentArenaManager.teamBlueScore++;
                    print("teamBlueScore = " + agentArenaManager.teamBlueScore);
                }

                fighterStatus.ArtifactCaptured();
                collider.transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();

                if(agentArenaManager.teamRedScore == 1 || agentArenaManager.teamBlueScore == 1)
                {
                    agentArenaManager.teamRedScore = 0;
                    agentArenaManager.teamBlueScore = 0;
                    fighterAgent.MatchEnded();
                }
            }
        }
    }

    public void RestoreOrigin()
    {
        transform.parent = artifactParent;
        transform.position = origin;
    }
}
