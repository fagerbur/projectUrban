using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStatus : MonoBehaviour
{
    public ArenaManager arenaManager;
    public int fighterTeam = 1;
    public bool fighterCapturedArtifact = false;

    private float fighterHealth = 3;
    private Coroutine respawnCoroutine;

    public void Awake() 
    {
        arenaManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ArenaManager>();
    }

    public void WeaponDamage(float fighterDamage)
    {
        fighterHealth -= fighterDamage;

        if(fighterHealth == 0)
        {
            fighterCapturedArtifact = false;
            
            respawnCoroutine = StartCoroutine(FighterRespawn());
        }
    }

    IEnumerator FighterRespawn()
    {
        Vector3 respawnLocation = arenaManager.RespawnLocation(fighterTeam);
        
        yield return new WaitForSeconds(3);

        if(transform.GetChild(1).childCount > 0)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();
        }

        transform.position = respawnLocation;
        transform.LookAt(new Vector3(0,2,0));
        fighterHealth = 3;

        yield return new WaitForFixedUpdate();
    }

    public void ArtifactCaptured()
    {
        if(fighterCapturedArtifact)
        {
            fighterCapturedArtifact = false;
        }
        else
        {
            fighterCapturedArtifact = true;
        }
    }
}
