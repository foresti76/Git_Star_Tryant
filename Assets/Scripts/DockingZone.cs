using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingZone : MonoBehaviour {
    Controls uIControls;

	// Use this for initialization
	void Start () {
        uIControls = FindObjectOfType<Controls>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            uIControls.ShowDockingPrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            uIControls.HideDockingPrompt();
        }
    }
}
