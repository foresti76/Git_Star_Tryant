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
    public int slotID;
    private float nextFire;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Mover shotScript = shot.GetComponent<Mover>();
            shotScript.damage = shotDamage;
            shotScript.firer = "player";
        }
    }
}
