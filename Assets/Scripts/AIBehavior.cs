using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour {

    public GameObject target;

    //movement effects
    public ParticleSystem engineFlare;
    public ParticleSystem engineCore;
    public ParticleSystem rightManuverJetRight;
    public ParticleSystem rightManuverJetLeft;
    public ParticleSystem leftManuverJetLeft;
    public ParticleSystem leftManuverJetRight;
    public ParticleSystem frontJet;

    UnityEngine.AI.NavMeshAgent agent;
    GameObject player;

    //todo bring in the ship data



    //variables to tell which way i am moving
    bool movingForward;
    bool turningLeft;
    bool turningRight;
    bool stopping;
 
    // Use this for initialization
    void Start()
    {
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player;
        //agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);

        //set off the effects when you are turning
        if (movingForward)
        {
            var coreMain = engineCore.main;
            var flareMain = engineFlare.main;
            coreMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            flareMain.startRotationZ = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
            engineFlare.gameObject.SetActive(true);
            engineCore.gameObject.SetActive(true);
        }
        else
        {
            engineFlare.gameObject.SetActive(false);
            engineCore.gameObject.SetActive(false);
        }

        if (stopping)
        {
            var frontMain = frontJet.main;
            frontMain.startRotationZ = (transform.rotation.eulerAngles.y + 180) * Mathf.Deg2Rad;
            frontJet.gameObject.SetActive(true);
        }
        else
        {
            frontJet.gameObject.SetActive(false);
        }

        if (turningRight)
        {
            var rjrMain = rightManuverJetRight.main;
            rjrMain.startRotationZ = (transform.rotation.eulerAngles.y - 90) * Mathf.Deg2Rad;
            var rjlMain = rightManuverJetLeft.main;
            rjlMain.startRotationZ = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
            rightManuverJetRight.gameObject.SetActive(true);
            rightManuverJetLeft.gameObject.SetActive(true);

        }
        else
        {
            rightManuverJetRight.gameObject.SetActive(false);
            rightManuverJetLeft.gameObject.SetActive(false);
        }

        if (turningLeft)
        {
            var ljlMain = leftManuverJetLeft.main;
            var ljrMain = leftManuverJetRight.main;
            ljlMain.startRotationZ = (transform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
            ljrMain.startRotationZ = (transform.rotation.eulerAngles.y - 90) * Mathf.Deg2Rad;
            leftManuverJetLeft.gameObject.SetActive(true);
            leftManuverJetRight.gameObject.SetActive(true);


        }
        else
        {
            leftManuverJetLeft.gameObject.SetActive(false);
            leftManuverJetRight.gameObject.SetActive(false);
        }
    }

           
        
}

