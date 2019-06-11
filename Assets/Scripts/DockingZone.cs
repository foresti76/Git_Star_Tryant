using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingZone : MonoBehaviour {

    public int starbaseID;
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
            //todo setup the UI to have this stations information in the starbase screen.
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
