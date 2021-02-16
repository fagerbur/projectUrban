using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureBase : MonoBehaviour
{
    private int artifactTeam;
    private string teamName;

    public bool artifactInBase = true;
    public GameObject gameManager;
    public ArenaManager arenaManager;
    public ArenaManager_AgentTrainer agentArenaManager;

    void Start()
    {
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
        agentArenaManager = gameManager.GetComponent<ArenaManager_AgentTrainer>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.name.Contains("fighter"))
        {
            FighterAgent fighterAgent = collider.gameObject.GetComponent<FighterAgent>();

            AgentStatus fighterStatus = collider.gameObject.GetComponent<AgentStatus>();

            if (fighterStatus.fighterCapturedArtifact && artifactTeam != fighterStatus.fighterTeam)
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
                if(collider.transform.GetChild(1).childCount > 0)
                {
                    collider.transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();
                }

                if(agentArenaManager.teamRedScore == 1 || agentArenaManager.teamBlueScore == 1)
                {
                    agentArenaManager.teamRedScore = 0;
                    agentArenaManager.teamBlueScore = 0;
                    fighterAgent.MatchEnded();
                }
            }
        }
    }
}
