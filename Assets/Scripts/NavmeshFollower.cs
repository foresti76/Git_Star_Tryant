using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshFollower : MonoBehaviour {

    public GameObject target;

    NavMeshAgent agent;
    GameObject player;
    float speed;
    // Use this for initialization
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(target.transform.position);

        //todo add in movement
	}
}
