using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStatus : MonoBehaviour
{
    public ArenaManager arenaManager;
    public int fighterTeam = 1;
    public bool fighterCapturedArtifact = false;

    private float fighterHealth = 3;

    public void Awake() 
    {
        arenaManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ArenaManager>();
        fighterTeam = Random.Range(0,1);
    }

    public void WeaponDamage(float fighterDamage)
    {
        fighterHealth -= fighterDamage;

        if(fighterHealth == 0)
        {
            fighterCapturedArtifact = false;
            
            FighterRespawn();
        }
    }

    public void FighterRespawn()
    {

        Vector3 spawnLocation = arenaManager.SpawnLocation(fighterTeam);
        
        if(transform.GetChild(1).childCount > 0)
        {
            transform.GetChild(1).transform.GetChild(0).GetComponent<CaptureArtifact>().RestoreOrigin();
        }

        transform.position = spawnLocation;
        transform.LookAt(new Vector3(0,2,0));
        fighterHealth = 3;

        Physics.SyncTransforms();
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
