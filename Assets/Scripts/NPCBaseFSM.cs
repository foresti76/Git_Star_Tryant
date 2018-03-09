using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBaseFSM : StateMachineBehaviour {


    public GameObject NPC;
    public bool IsAggro;
    public GameObject target;
    public float accuracy;
    public AIBehavior Ai;
    public bool speedingUp;
    public bool turningLeft;
    public bool turningRight;
    public bool slowingDown;
    public bool stopping;
    public UnityEngine.AI.NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        accuracy = 15.0f;
        Ai = NPC.GetComponent<AIBehavior>();
        agent = NPC.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
}
