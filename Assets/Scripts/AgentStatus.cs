using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;

public class AgentStatus : MonoBehaviour
{
    public ArenaManager_AgentTrainer arenaManager;
    public FighterAgent fighterAgent;
    public int fighterTeam = 1;
    public bool fighterCapturedArtifact = false;

    private float fighterHealth = 3;

    public void Awake() 
    {
        arenaManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ArenaManager_AgentTrainer>();
        fighterTeam = Random.Range(0,1);
        fighterAgent = GetComponent<FighterAgent>();
    }

    public void WeaponDamage(float fighterDamage)
    {
        fighterHealth -= fighterDamage;

        if(fighterHealth == 0)
        {
            fighterCapturedArtifact = false;
            FighterRespawn();
            fighterAgent.AgentDestroyedAgent();
        }
    }

    public void FighterRespawn()
    {
        GetComponent<BehaviorParameters>().TeamId = fighterTeam;

        Vector3 spawnLocation = arenaManager.SpawnLocation(fighterTeam);
        
        if(transform.GetChild(1).childCount > 0)
        {
            fighterAgent.AgentDroppedArtifact();
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
