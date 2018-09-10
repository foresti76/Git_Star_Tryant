using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour {

    public int maxPullMass;
    public int pullRange;
    public int pullRate;
    public int energyCost;
    public LineRenderer tractorBeamLineRenderer;
    public bool tractorBeamEngaged;

    GameObject tractorTarget;
    LineRenderer tb;
    Generator myShipGenerator;
    // Use this for initialization
    void Start () {
        myShipGenerator = this.GetComponent<Generator>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(tractorBeamEngaged && Vector3.Distance(transform.position, tractorTarget.transform.position) <= pullRange && myShipGenerator.currentPower >= energyCost)
        {
            myShipGenerator.currentPower -= energyCost;
            tractorTarget.transform.position = Vector3.MoveTowards(tractorTarget.transform.position, this.transform.position, Time.deltaTime * pullRate);
            tb.SetPosition(0, transform.position);
            tb.SetPosition(1, tractorTarget.transform.position);
            if(tractorTarget.tag == "Loot")
            {
                tractorTarget.GetComponent<LootObject>().isBeingTractored = true;
            }
        } else if (tractorBeamEngaged && (Vector3.Distance(transform.position, tractorTarget.transform.position) > pullRange || myShipGenerator.currentPower < energyCost || tractorTarget == null))
        {
            tractorBeamEngaged = false;
            DisngageTractorBeam();
        }
	}

    public void EngageTractorBeam(GameObject target)
    {
        tractorTarget = target;
        if(target.GetComponent<Rigidbody>().mass <= maxPullMass 
            && Vector3.Distance(transform.position, tractorTarget.transform.position) <= pullRange)
        {
            tractorBeamEngaged = true;
            tb = Instantiate(tractorBeamLineRenderer, transform.parent);
            tb.SetPosition(0, transform.position);
            tb.SetPosition(1, tractorTarget.transform.position);
        }
    }

    public void DisngageTractorBeam()
    {
        tractorBeamEngaged = false;
        Destroy(tb.gameObject);
        if (tractorTarget.tag == "Loot")
        {
            tractorTarget.GetComponent<LootObject>().isBeingTractored = false;
        }
        tractorTarget = null;
    }
}
