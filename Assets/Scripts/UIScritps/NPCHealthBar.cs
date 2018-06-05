using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCHealthBar : MonoBehaviour {
    public GameObject myShip;

    private Image image;
    private Hull myHull;
    private Shield myShield;
    private bool hasShield = true;
    private float maxShield;
    private float maxHull;
    private Transform parent;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
        myHull = myShip.GetComponent<Hull>();
        myShield = myShip.GetComponentInChildren<Shield>();
        maxHull = myHull.maxHull;
        maxShield = myShield.maxShield;
        parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        if (!myShip)
            Destroy(parent.gameObject);

        parent.position = myShip.transform.position + new Vector3(0, 0, 2);

        if (!myShield.shieldDown)
        {
            image.color = Color.blue;
            image.fillAmount = myShield.currentShield / maxShield;
        }

        if (myShield.shieldDown)
        {
            image.color = Color.red;
            image.fillAmount = myHull.curHull / maxHull;
        }


	}
}
