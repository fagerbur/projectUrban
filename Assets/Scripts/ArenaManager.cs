using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    private Object teamBase;
    private List<Transform> team0Spawns;
    private List<Transform> team1Spawns; 

    void Awake()
    {
        team0Spawns = new List<Transform>();
        team1Spawns = new List<Transform>();

        Vector3 teamBase0Pos = new Vector3(237.1f, -7.45f, -221.7f);
        Vector3 teamBase1Pos = new Vector3(-237.1f, -7.45f, 221.7f);

        teamBase = Resources.Load("Arena/TeamBase", typeof(GameObject));

        GameObject teamBase0 = (GameObject)Instantiate(teamBase, teamBase0Pos, Quaternion.identity);
        teamBase0.name = "teamBase0";
        GameObject teamBase1 = (GameObject)Instantiate(teamBase, teamBase1Pos, Quaternion.AngleAxis(180, Vector3.up));
        teamBase1.name = "teamBase1";

        Transform team0SpawnBase = teamBase0.transform.GetChild(0);
        Transform team1SpawnBase = teamBase1.transform.GetChild(0);

        foreach (Transform child in team0SpawnBase)
        {
            team0Spawns.Add(child);
        }

        foreach (Transform child in team1SpawnBase)
        {
            team1Spawns.Add(child);
        }
    }

    void Update()
    {
        
    }

    public Vector3 RespawnLocation(int fighterTeam)
    {
        Collider[] isColliding;
        int randomSpawn = Random.Range(0,3);

        if(fighterTeam == 0)
        {
            isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,2,2));
            while(isColliding.Length > 1)
            {
                randomSpawn = Random.Range(0,3);
                isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,2,2));
            }

            return new Vector3(team0Spawns[randomSpawn].position.x, 3, team0Spawns[randomSpawn].position.z);
        }
        else
        {
            isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,2,2));
            while(isColliding.Length > 1)
            {
                randomSpawn = Random.Range(0,3);
                isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,2,2));
            }

            return new Vector3(team1Spawns[randomSpawn].position.x, 3, team1Spawns[randomSpawn].position.z);
        }
    }
}
