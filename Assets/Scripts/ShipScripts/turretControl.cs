using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretControl : MonoBehaviour {

   
    WeaponController myWeaponController;


    void Start()
    {
        myWeaponController = this.GetComponent<WeaponController>();
    }
	// Update is called once per frame
	void Update () {
        this.update_rotation_y();
    }

    void update_rotation_y()
    {

        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos.x = mousePos.x * Screen.width;
        mousePos.y = mousePos.y * Screen.height;

        Vector3 turretPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 targetDir = mousePos - turretPos;
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) * Mathf.Rad2Deg;
        //get angle of ship and compare and clamp
        float angleDelta = angle - transform.parent.eulerAngles.y;

        if(angleDelta < -180)
        {
            angleDelta += 360;
        }
        
        if (angleDelta > turretRotationLimit)
        {
            angleDelta = turretRotationLimit;
        } else if (angleDelta < -turretRotationLimit)
        {
            angleDelta = -turretRotationLimit;
        }

        angle = transform.parent.eulerAngles.y + angleDelta;
        
        Quaternion targetRot = Quaternion.Euler(0, angle, 0);
        Quaternion rot = Quaternion.Euler(transform.eulerAngles);

        // turn the turret in the desired direction scaled by the turret rotation dampaning value.
        transform.rotation = Quaternion.RotateTowards(rot, targetRot, turretRotationRate * Time.deltaTime);

    }
}
