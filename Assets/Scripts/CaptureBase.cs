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
        arenaManager = gameManager.GetComponent<ArenaManager>();
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if(collider.gameObject.name.Contains("fighter"))
        {
            FighterStatus fighterStatus = collider.gameObject.GetComponent<FighterStatus>();

            if (fighterStatus.fighterCapturedArtifact && artifactTeam != fighterStatus.fighterTeam)
            {
                arenaManager.SetAiTeammatesCapture(fighterStatus.fighterTeam);

                if(artifactTeam == 0)
                {
                    arenaManager.teamRedScore++;
                    print("teamRedScore = " + arenaManager.teamRedScore);
                }
                else
                {
                    arenaManager.teamBlueScore++;
                    print("teamBlueScore = " + arenaManager.teamBlueScore);
                }

                if(collider.transform.GetChild(1).childCount > 0)
                {
                    collider.transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();
                }

                if(arenaManager.teamRedScore == 3 || arenaManager.teamBlueScore == 3)
                {
                    print("Final Score: " + arenaManager.teamBlueScore + " Blue - " + arenaManager.teamRedScore + " Red");
                    arenaManager.teamRedScore = 0;
                    arenaManager.teamBlueScore = 0;
                }
            }
        }
    }
}
