using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStatus : MonoBehaviour
{
    public ArenaManager arenaManager;
    private float fighterHealth = 3;
    private bool fighterCapturedArtifact = false;
    private int fighterTeam = 1;
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

        transform.position = respawnLocation;
        transform.LookAt(new Vector3(0,2,0));
        fighterHealth = 3;

        yield return new WaitForFixedUpdate();
    }

    public void ArtifactCaptured()
    {
        fighterCapturedArtifact = true;
    }
}
