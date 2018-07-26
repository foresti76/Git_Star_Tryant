using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMover : MonoBehaviour {

    public Transform parent;

    // Use this for initialization
	
	// Update is called once per frame
	void Update () {
        if (parent)
        {
            transform.position = parent.position;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
	}
}
