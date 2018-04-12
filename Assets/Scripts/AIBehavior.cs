using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;
    public float accuracy;


    Hull hull;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        //agent.speed;
        anim = GetComponent<Animator>();
        anim.SetBool("IsPatrolling", true);
        hull = GetComponent<Hull>();
    }

    public void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
        anim.SetBool("IsAggro", true);
    }
    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("Hull", hull.curHull / hull.maxHull);
        if(!target && anim.GetBool("IsAggro"))
        {
            anim.SetBool("IsAggro", false);
        }
    }
        
}

