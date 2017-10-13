using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour {

    public float currentShield;
    public float shieldRechargeDuration;
    public float shieldRefreshDuration;
    public float maxShield;
    public float shieldRechageRate;
    public GameObject shieldObject;
    public float shieldDisplayDuration;

    private float shieldDisplayTime;
    private Renderer shieldRendere;
    private Collider shieldCollider;
    private bool shieldDown = false;
    private float shieldRechargeTime;
    // Use this for initialization
    void Start () {
        currentShield = maxShield;
        shieldRendere = shieldObject.GetComponent<Renderer>();
        shieldRendere.enabled = false;
        shieldCollider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {
        // If the shield is reduced to zero it takes longer before it will start recharging
		if (currentShield <= 0 && !shieldDown)
        {
            shieldDown = true;
            currentShield = 0;
            shieldRechargeTime = Time.time + shieldRefreshDuration;
            shieldCollider.enabled = false;
        }
        // Start recharging the sheild if enough time has passed and it has taken damage.
        if (Time.time >= shieldRechargeTime  && currentShield < maxShield)
        {
            if (shieldDown)
            {
                shieldDown = false;
                shieldRendere.enabled = true;
                shieldDisplayTime = Time.time + shieldDisplayDuration;
            }

            currentShield += shieldRechageRate;
            if(shieldCollider.enabled == false)
            {
                shieldCollider.enabled = true;
            }
            // Make sure the shield does not exceed its max value
            if (currentShield > maxShield)
            {
                currentShield = maxShield;
            }
        }
        //turn off the shield object once it has been turned on.
        if (Time.time >= shieldDisplayTime)
        {
            shieldRendere.enabled = false;
        }
    }
    //check to see if we have a shield not currently used
    public bool HasShield()
    {
        if (currentShield > 0)
        {
            return true;
        }
        return false;
    }
    //damage the shield
    public void DamageShield(float damage)
    {
        //display the shield object
        shieldRendere.enabled = true;
        // reduce the shield value;
        currentShield -= damage;
        // set the time we can start recharging
        shieldRechargeTime = Time.time + shieldRechargeDuration;
        shieldDisplayTime = Time.time + shieldDisplayDuration;
    }
}
