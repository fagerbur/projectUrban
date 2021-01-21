using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class FighterAgent : Agent
{
    public float forceMultiplier = 10;
    public Transform EnemyArtifact;
    public Transform TeamBase;
    private Rigidbody fighterBody;
    private AgentStatus agentStatus;

    void Start()
    {
        fighterBody = GetComponent<Rigidbody>();
        agentStatus = GetComponent<AgentStatus>();
    }

    public override void OnEpisodeBegin()
    {
        //Should the arena restart all the agents once a score of 3 is reached?
        this.fighterBody.angularVelocity = Vector3.zero;
        this.fighterBody.velocity = Vector3.zero;
        agentStatus.FighterRespawn();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(EnemyArtifact.localPosition);
        sensor.AddObservation(TeamBase.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(fighterBody.velocity.x);
        sensor.AddObservation(fighterBody.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        fighterBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, EnemyArtifact.localPosition);

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
