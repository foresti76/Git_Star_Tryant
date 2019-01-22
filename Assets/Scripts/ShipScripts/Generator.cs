using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public float currentPower;
    public float maxPower;
    public float regenRate;
 
    
    void Start ()
    {
        currentPower = maxPower;
    }   
	// Update is called once per frame
	void LateUpdate () {
		if (currentPower < maxPower)
        {
            currentPower += regenRate;
        }

        if (currentPower > maxPower)
        {
            currentPower = maxPower;
        }

        if (currentPower < 0)
        {
            currentPower = 0;
        }
    }
}
