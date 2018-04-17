using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour {

    public LineRenderer lr;
    public GameObject target;
    public int firingTime;

    public bool firingLaser = false;
    LineRenderer currentLaser;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ActivateLaser(GameObject target)
    {
        firingLaser = true;
        currentLaser = Instantiate(lr, transform.parent);
        currentLaser.SetPosition(0, transform.position);
        currentLaser.SetPosition(1, target.transform.position);
        Invoke("AwardStuff", firingTime);
        Invoke("DeactivateLaser", firingTime);
    }

    void DeactivateLaser()
    {
        Destroy(currentLaser.gameObject);
        target = null;
        firingLaser = false;
    }

    void AwardStuff()
    {
        //todo give stuff after mining is complete
    }
}
