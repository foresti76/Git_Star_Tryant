using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyEffectObject : MonoBehaviour {
    ParticleSystem myParticleSystem;
    float timeToDie;

	// Use this for initialization
	void Start () {
        myParticleSystem = GetComponent<ParticleSystem>();
        timeToDie = Time.time + myParticleSystem.main.duration;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= timeToDie)
        {
            Destroy(this.gameObject);
        }
	}
}
