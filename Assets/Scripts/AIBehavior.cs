using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;
    public float accuracy;
    public float fleeValue;
    public GameObject healthbarPrefab;

    Hull hull;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        //agent.speed;
        anim = GetComponent<Animator>();
        anim.SetBool("IsPatrolling", true);
        hull = GetComponent<Hull>();
        GameObject myHealthbar = Instantiate(healthbarPrefab, transform.position, Quaternion.Euler(90f,0.0f,0.0f));
        myHealthbar.GetComponentInChildren<NPCHealthBar>().myShip = this.gameObject;
        
    }

    public void UpdateTarget(GameObject newTarget)
    {
        target = newTarget;
        anim.SetBool("IsAggro", true);
    }
    // Update is called once per frame
    void Update()
    {

        if(target && hull.curHull / hull.maxHull <= fleeValue)
        {
            anim.SetBool("IsFleeing", true);
        }

        if(!target && anim.GetBool("IsAggro"))
        {
            anim.SetBool("IsAggro", false);
        }
    }
        
}

