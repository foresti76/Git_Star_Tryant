using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Slider playerShieldHUD;
    public Slider playerHullHUD;
    public Slider playerPowerHUD;

    GameObject player;
    Hull hull;
    ShipGenerator generator;
    ShieldBehavior shield;
    float shieldDisplayValue;
    float hullDisplayValue;
    float powerDisplayValue;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        hull = player.GetComponent<Hull>();
        generator = player.GetComponent<ShipGenerator>();
        shield = player.GetComponentInChildren<ShieldBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
        playerHullHUD.value = hull.curHull / hull.maxHull;
        playerPowerHUD.value = generator.currentPower / generator.maxPower;
        playerShieldHUD.value = shield.currentShield / shield.maxShield;
	}
}
