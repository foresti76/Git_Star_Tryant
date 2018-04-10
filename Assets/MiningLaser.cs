using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour {

    public LineRenderer lr;
    public GameObject target;
    public int firingTime;

    bool firingLaser = false;
    LineRenderer currentLaser;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Keypad0) && firingLaser == false && GetComponentInParent<Rigidbody>().velocity.magnitude == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.CompareTag("Asteroid"))
                {
                    target = hit.transform.gameObject;
                    firingLaser = true;
                    ActivateLaser();
                }
            }
        }
	}

    void ActivateLaser()
    {
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

    }
}
