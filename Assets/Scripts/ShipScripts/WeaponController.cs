using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public GameObject shot;
    public int shotDamage;
    //public float shotSpeed;
    public Transform shotSpawn;
    public float fireRate;
    public float energyCost;
    public int slotID;
    public bool firing = false;
    public int turretRotationRate = 1;
    public float turretRotationLimit = 30;
    public Vector3 turretPos;

    private float nextFire;
    private ShipGenerator myShipGenerator; 
    // Use this for initialization
    void Start()
    {
        myShipGenerator = this.GetComponentInParent<ShipGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firing && Time.time >= nextFire && myShipGenerator.currentPower >= energyCost)
        {
            myShipGenerator.currentPower -= energyCost;
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Mover shotScript = shot.GetComponent<Mover>();
            shotScript.damage = shotDamage;
            shotScript.firer = "player";
        }
    }
}
