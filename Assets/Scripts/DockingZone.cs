using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingZone : MonoBehaviour {
    Controls uIControls;
    ArenaManager arenaManager;

	// Use this for initialization
	void Start () {
        uIControls = FindObjectOfType<Controls>();
        arenaManager = FindObjectOfType<ArenaManager>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && arenaManager.arenaActive == false)
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
