using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager_AgentTrainer : MonoBehaviour
{
    private Object teamBase0;
    private Object teamBase1;
    private List<Transform> team0Spawns;
    private List<Transform> team1Spawns;

    void Awake()
    {
        team0Spawns = new List<Transform>();
        team1Spawns = new List<Transform>();

        Vector3 teamBase0Pos = new Vector3(237.1f, -7.45f, -221.7f);
        Vector3 teamBase1Pos = new Vector3(-237.1f, -7.45f, 221.7f);

        teamBase0 = Resources.Load("Arena/TeamBase0", typeof(GameObject));
        teamBase1 = Resources.Load("Arena/TeamBase1", typeof(GameObject));

        GameObject teamBaseRed = (GameObject)Instantiate(teamBase0, teamBase0Pos, Quaternion.identity);
        teamBaseRed.name = "teamBaseRed";
        GameObject teamBaseBlue = (GameObject)Instantiate(teamBase1, teamBase1Pos, Quaternion.AngleAxis(180, Vector3.up));
        teamBaseBlue.name = "teamBaseBlue";

        Transform team0SpawnBase = teamBaseRed.transform.GetChild(0);
        Transform team1SpawnBase = teamBaseBlue.transform.GetChild(0);

        foreach (Transform child in team0SpawnBase)
        {
            team0Spawns.Add(child);
        }

        foreach (Transform child in team1SpawnBase)
        {
            team1Spawns.Add(child);
        }

        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        Object fighterZeroAi = Resources.Load("Fighters/fighterZero_AI", typeof(GameObject));

        GameObject fighterZeroAi1 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);
        GameObject fighterZeroAi2 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);
        GameObject fighterZeroAi3 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);
        GameObject fighterZeroAi4 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);
        GameObject fighterZeroAi5 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);
        GameObject fighterZeroAi6 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity);

        int AgentTeam = fighterZeroAi1.GetComponent<FighterStatus>().fighterTeam;

        fighterZeroAi2.GetComponent<FighterStatus>().fighterTeam = AgentTeam;
        fighterZeroAi3.GetComponent<FighterStatus>().fighterTeam = AgentTeam;
        fighterZeroAi4.GetComponent<FighterStatus>().fighterTeam = 1 - Mathf.Abs(AgentTeam);
        fighterZeroAi5.GetComponent<FighterStatus>().fighterTeam = 1 - Mathf.Abs(AgentTeam);
        fighterZeroAi6.GetComponent<FighterStatus>().fighterTeam = 1 - Mathf.Abs(AgentTeam);

        fighterZeroAi1.GetComponent<FighterStatus>().FighterRespawn();
        fighterZeroAi2.GetComponent<FighterStatus>().FighterRespawn();
        fighterZeroAi3.GetComponent<FighterStatus>().FighterRespawn();
        fighterZeroAi4.GetComponent<FighterStatus>().FighterRespawn();
        fighterZeroAi5.GetComponent<FighterStatus>().FighterRespawn();
        fighterZeroAi6.GetComponent<FighterStatus>().FighterRespawn();
    }

    public Vector3 SpawnLocation(int fighterTeam)
    {
        Collider[] isColliding;
        int randomSpawn = Random.Range(0,3);

        if(fighterTeam == 1)
        {
            isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,4,2));
            while(isColliding.Length > 2)
            {
                randomSpawn = Random.Range(0,3);
                isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,4,2));
            }

            return new Vector3(team0Spawns[randomSpawn].position.x, 3, team0Spawns[randomSpawn].position.z);
        }
        else
        {
            isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,4,2));
            while(isColliding.Length > 2)
            {
                randomSpawn = Random.Range(0,3);
                isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,4,2));
            }

            return new Vector3(team1Spawns[randomSpawn].position.x, 3, team1Spawns[randomSpawn].position.z);
        }
    }
}
