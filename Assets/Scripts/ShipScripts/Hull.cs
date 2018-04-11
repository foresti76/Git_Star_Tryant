using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour {

    public float curHull;
    public float maxHull;
    public GameObject explosion;
    public int armor;

	// Update is called once per frame
	void Update () {
		if(curHull < 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
	}

    public void DoDamage(float dam, GameObject attacker)
    {
            curHull -= (dam - armor);
        if (GetComponent<AIBehavior>())
        {
            AIBehavior myAIBehavior = transform.GetComponentInParent<AIBehavior>();
            myAIBehavior.UpdateTarget(attacker);
        }
    }
}
