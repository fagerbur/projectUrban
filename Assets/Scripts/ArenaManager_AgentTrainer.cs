using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager_AgentTrainer : MonoBehaviour
{
    private Object teamBase0;
    private Object teamBase1;
    private List<Transform> team0Spawns;
    private List<Transform> team1Spawns;
    private GameObject teamBaseRed;
    private GameObject teamBaseBlue;

    public Transform FighterArray;
    public int teamRedScore = 0;
    public int teamBlueScore = 0;

    void Awake()
    {
        team0Spawns = new List<Transform>();
        team1Spawns = new List<Transform>();

        Vector3 teamBase0Pos = new Vector3(185f, -7.45f, -100f);
        Vector3 teamBase1Pos = new Vector3(-60f, -7.45f, 115f);

        teamBase0 = Resources.Load("Arena/TeamBase0", typeof(GameObject));
        teamBase1 = Resources.Load("Arena/TeamBase1", typeof(GameObject));

        teamBaseRed = (GameObject)Instantiate(teamBase0, teamBase0Pos, Quaternion.identity);
        teamBaseRed.name = "teamBaseRed";
        teamBaseBlue = (GameObject)Instantiate(teamBase1, teamBase1Pos, Quaternion.AngleAxis(180, Vector3.up));
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

    public void SpawnPlayers()
    {
        Object fighterZeroAi = Resources.Load("Fighters/fighterZero_AI", typeof(GameObject));

        GameObject fighterZeroAi1 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);
        GameObject fighterZeroAi2 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);
        GameObject fighterZeroAi3 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);
        GameObject fighterZeroAi4 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);
        GameObject fighterZeroAi5 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);
        GameObject fighterZeroAi6 = (GameObject)Instantiate(fighterZeroAi, new Vector3(0,-5,0), Quaternion.identity, FighterArray);

        fighterZeroAi1.GetComponent<AgentStatus>().fighterTeam = 0;
        fighterZeroAi1.GetComponent<FighterAgent>().EnemyArtifact = teamBaseRed.transform.GetChild(1);
        fighterZeroAi1.GetComponent<FighterAgent>().TeamBase = teamBaseBlue.transform.GetChild(2);
        fighterZeroAi1.GetComponent<AgentStatus>().FighterRespawn();

        fighterZeroAi2.GetComponent<AgentStatus>().fighterTeam = 0;
        fighterZeroAi2.GetComponent<FighterAgent>().EnemyArtifact = teamBaseRed.transform.GetChild(1);
        fighterZeroAi2.GetComponent<FighterAgent>().TeamBase = teamBaseBlue.transform.GetChild(2);
        fighterZeroAi2.GetComponent<AgentStatus>().FighterRespawn();

        fighterZeroAi3.GetComponent<AgentStatus>().fighterTeam = 0;
        fighterZeroAi3.GetComponent<FighterAgent>().EnemyArtifact = teamBaseRed.transform.GetChild(1);
        fighterZeroAi3.GetComponent<FighterAgent>().TeamBase = teamBaseBlue.transform.GetChild(2);
        fighterZeroAi3.GetComponent<AgentStatus>().FighterRespawn();

        fighterZeroAi4.GetComponent<AgentStatus>().fighterTeam = 1;
        fighterZeroAi4.GetComponent<FighterAgent>().EnemyArtifact = teamBaseBlue.transform.GetChild(1);
        fighterZeroAi4.GetComponent<FighterAgent>().TeamBase = teamBaseRed.transform.GetChild(2);
        fighterZeroAi4.GetComponent<AgentStatus>().FighterRespawn();

        fighterZeroAi5.GetComponent<AgentStatus>().fighterTeam = 1;
        fighterZeroAi5.GetComponent<FighterAgent>().EnemyArtifact = teamBaseBlue.transform.GetChild(1);
        fighterZeroAi5.GetComponent<FighterAgent>().TeamBase = teamBaseRed.transform.GetChild(2);
        fighterZeroAi5.GetComponent<AgentStatus>().FighterRespawn();

        fighterZeroAi6.GetComponent<AgentStatus>().fighterTeam = 1;
        fighterZeroAi6.GetComponent<FighterAgent>().EnemyArtifact = teamBaseBlue.transform.GetChild(1);
        fighterZeroAi6.GetComponent<FighterAgent>().TeamBase = teamBaseRed.transform.GetChild(2);
        fighterZeroAi6.GetComponent<AgentStatus>().FighterRespawn();
    }

    public Vector3 SpawnLocation(int fighterTeam)
    {
        Collider[] isColliding;
        int randomSpawn = Random.Range(0,4);
        int tries = 0;

        if(fighterTeam == 1)
        {
            isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,4,2));
            while(isColliding.Length > 2)
            {
                randomSpawn = Random.Range(0,4);
                isColliding = Physics.OverlapBox(team0Spawns[randomSpawn].position, new Vector3(2,4,2));
                if(tries > 10) break;
            }

            return new Vector3(team0Spawns[randomSpawn].position.x, 3, team0Spawns[randomSpawn].position.z);
        }
        else
        {
            isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,4,2));
            while(isColliding.Length > 2)
            {
                randomSpawn = Random.Range(0,4);
                isColliding = Physics.OverlapBox(team1Spawns[randomSpawn].position, new Vector3(2,4,2));
                tries++;
                if(tries > 10) break;
            }

            return new Vector3(team1Spawns[randomSpawn].position.x, 3, team1Spawns[randomSpawn].position.z);
        }
    }
}
