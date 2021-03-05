using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.Barracuda;

public class FighterAgent : Agent
{
    public Transform EnemyArtifact;
    public Transform TeamBase;
    private Rigidbody fighterBody;
    private AgentStatus agentStatus;
    private FighterWeaponFire fighterWeaponFire;
    private float powerInput;
    private float turnInput;
    private CityGenerator cityGenerator;

    public NNModel CaptureArtifact;
    public NNModel ReturnArtifact;

    public GameObject gameManager;
    public float moveSpeed = 5;
    public float speed = 100f;
    public float forceMultiplier = 10f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;

    private string fighterCaptureBehavior = "CaptureTraining";
    private string fighterReturnBehavior = "ReturnTraining";

    void Start()
    {
        fighterBody = GetComponent<Rigidbody>();
        agentStatus = GetComponent<AgentStatus>();
        fighterWeaponFire = GetComponentInChildren<FighterWeaponFire>();
        gameManager = GameObject.Find("GameManager");
        cityGenerator = gameManager.GetComponent<CityGenerator>();
        SetModel(fighterCaptureBehavior, CaptureArtifact);
    }

    public override void Initialize() 
    {
        var modelOverrider = GetComponent<ModelOverrider>();
        if (modelOverrider.HasOverrides)
        {
            CaptureArtifact = modelOverrider.GetModelForBehaviorName(fighterCaptureBehavior);
            fighterCaptureBehavior = ModelOverrider.GetOverrideBehaviorName(fighterCaptureBehavior);

            ReturnArtifact = modelOverrider.GetModelForBehaviorName(fighterReturnBehavior);
            fighterReturnBehavior = ModelOverrider.GetOverrideBehaviorName(fighterReturnBehavior);
        }    
    }

    public override void OnEpisodeBegin()
    {
        this.fighterBody.angularVelocity = Vector3.zero;
        this.fighterBody.velocity = Vector3.zero;
        SetModel(fighterCaptureBehavior, CaptureArtifact);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(EnemyArtifact.position);
        sensor.AddObservation(TeamBase.position);

        sensor.AddObservation(transform.position);

        sensor.AddObservation(fighterBody.velocity.x);
        sensor.AddObservation(fighterBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(-0.0001f);

        var discreteActions = actionBuffers.DiscreteActions;

        MoveAgent(actionBuffers.DiscreteActions);

        discreteActions[0] = 0;
        discreteActions[1] = 0;
        discreteActions[2] = 0;
    }

    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var forwardAxis = act[0];
        var rotateAxis = act[1];

        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.forward;
                break;
            case 2:
                dirToGo = -transform.forward;
                break;
        }

        switch (rotateAxis)
        {
            case 1:
                rotateDir = -transform.up;
                break;
            case 2:
                rotateDir = transform.up;
                break;
        }

        if (act[2] == 1)
        {
            fighterWeaponFire.isFiring = true;
        }

        fighterBody.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);
    }

    public void AgentDestroyedAgent()
    {
        AddReward(0.25f);
        print("Fighter Kill: " + GetCumulativeReward());
    }

    public void AgentCapturedArtifact()
    {
        AddReward(1.0f);
        print("Artifact Stolen: " + GetCumulativeReward());
        foreach (Transform fighter in agentStatus.arenaManager.FighterArray)
        {
            if(fighter.GetComponent<AgentStatus>().fighterTeam == GetComponent<AgentStatus>().fighterTeam)
            {
                fighter.GetComponent<FighterAgent>().SetModel(fighterReturnBehavior, ReturnArtifact);
            }
        }
    }

    public void AgentDroppedArtifact()
    {
        AddReward(-0.5f);
        print("Artifact Dropped: " + GetCumulativeReward());
        foreach (Transform fighter in agentStatus.arenaManager.FighterArray)
        {
            if(fighter.GetComponent<AgentStatus>().fighterTeam == GetComponent<AgentStatus>().fighterTeam)
            {
                fighter.GetComponent<FighterAgent>().SetModel(fighterCaptureBehavior, CaptureArtifact);
            }
        }
    }

    public void AgentReturnedArtifact()
    {
        AddReward(3.0f);
        print("Artifact Captured: " + GetCumulativeReward());
        foreach (Transform fighter in agentStatus.arenaManager.FighterArray)
        {
            if(fighter.GetComponent<AgentStatus>().fighterTeam == GetComponent<AgentStatus>().fighterTeam)
            {
                fighter.GetComponent<FighterAgent>().SetModel(fighterCaptureBehavior, CaptureArtifact);
            }
        }
    }

    public void MatchEnded()
    {
        cityGenerator.DestroyAll();
        EndEpisode();

        foreach (Transform fighter in agentStatus.arenaManager.FighterArray)
        {
            fighter.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            fighter.GetComponent<Rigidbody>().velocity = Vector3.zero;
            fighter.transform.position = new Vector3(0,-100,0);
            fighter.GetComponent<AgentStatus>().FighterRespawn();
            fighter.GetComponent<FighterAgent>().SetModel(fighterCaptureBehavior, CaptureArtifact);
        }
        //cityGenerator.SpawnAll();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;
        discreteActions[1] = 0;

        if (Input.GetKey(KeyCode.W))
        {
            discreteActions[0] = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            discreteActions[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            discreteActions[1] = 2;
        }
        if (Input.GetKey(KeyCode.S))
        {   
            discreteActions[0] = 2;
        }
        discreteActions[2] = Input.GetKey(KeyCode.X) ? 1 : 0;
    }
}
