using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;
    public float accuracy;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        UpdateTarget(player);

        //agent.speed;
        anim = GetComponent<Animator>();
    }

    void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
         anim.SetBool("IsAggro", true);
    }
    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.transform.position)
    }
        
}

