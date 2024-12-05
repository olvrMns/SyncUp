using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAEnemy : MonoBehaviour
{

    NavMeshAgent agent;     
    Animator anim;          
    State currentState;     

    public Transform player; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();    
        anim = GetComponent<Animator>();         
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Idle(gameObject, agent, anim, player);
        //agent.speed = 0;
        //agent.acceleration = 0;
    }

    void Update()
    {
        currentState = currentState.Process(); 
    }
}