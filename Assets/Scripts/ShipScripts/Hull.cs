using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hull : MonoBehaviour {

    public float curHull;
    public float maxHull;
    public GameObject explosion;
    public GameObject lootObject;
    public int armor;

    Ship myShip;
    //bool isDead = false;

    void Start()
    {
        myShip = GetComponent<Ship>();
    }
	// Update is called once per frame
	void Update () {
		if(curHull <= 0)
        {
           // isDead = true;
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(lootObject, transform.position, transform.rotation);
            lootObject.GetComponentInChildren<LootObject>().CreateLoot(myShip.lootTable, myShip.lootAmount);
            lootObject.name = "Loot (" + this.name + ")"; 
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
