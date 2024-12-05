using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentSyncer : MonoBehaviour
{

    private AnimationSync animationSync;
    private NavMeshAgent agent;

    private float defaultAcceleration;
    private float defaultSpeed;  

    void Start()
    {
        if (animationSync == null) animationSync = GameObject.Find("AnimationSpeedSync").GetComponent<AnimationSync>();
        agent = GetComponent<NavMeshAgent>();
        defaultAcceleration = agent.acceleration;
        defaultSpeed = agent.speed;
    }

    void Update()
    {
        agent.speed = defaultSpeed * animationSync.AnimationSpeed;
        agent.acceleration = defaultAcceleration * animationSync.AnimationSpeed;
    }
}
