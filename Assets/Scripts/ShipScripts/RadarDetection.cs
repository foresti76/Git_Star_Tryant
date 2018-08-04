using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetection : MonoBehaviour {
    public Radar myRadar;

    Ship myShip;

	// Use this for initialization
	void Start () {
        myRadar = transform.GetComponentInParent<Radar>();
        myShip = transform.GetComponentInParent<Ship>();
	}


    void OnTriggerEnter(Collider other)
    {
        ECM otherECM = other.gameObject.GetComponent<ECM>();
        Radar otherRadar = other.GetComponent<Radar>();
        if (otherECM && Vector3.Distance(transform.position, other.transform.position) <= myRadar.range - otherECM.detectionDefense && other.tag != "Level")
        {
            Debug.Log(other.gameObject.name + " is detected on entry.");
            myRadar.detections.Add(other.gameObject);
            if (myShip.playerShip == true)
            {
                otherRadar.miniMapIcon.SetActive(true);
            }

        }    
    }

    void OnTriggerExit(Collider other)
    {
        Radar otherRadar = other.gameObject.GetComponent<Radar>();
        if (otherRadar && other.tag != "Player")
        {
            otherRadar.miniMapIcon.SetActive(false);
        }
        myRadar.detections.Remove(other.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        ECM otherECM = other.GetComponent<ECM>();
        Radar otherRadar = other.GetComponent<Radar>();

        if (myRadar.detections.Contains(other.gameObject) && otherECM && Vector3.Distance(transform.position, other.transform.position) <= myRadar.range - otherECM.detectionDefense && other.tag != "Level")
        {
            Debug.Log(other.gameObject.name + " is currently detected and in the list.");
            return;
        }
        else if (myRadar.detections.Contains(other.transform.gameObject) && otherECM && Vector3.Distance(transform.position, other.transform.position) > myRadar.range - otherECM.detectionDefense)
        {
            Debug.Log(other.gameObject.name + " is currently detected and has moved out of range for detection.");
            myRadar.detections.Remove(other.gameObject);
            if (myShip.playerShip == true)
            {
                otherRadar.miniMapIcon.SetActive(false);
            }
        }

        if (otherECM && Vector3.Distance(transform.position, other.transform.position) <= myRadar.range - otherECM.detectionDefense && other.tag != "Level")
        {
            Debug.Log(other.gameObject.name + " is now detected for the first time.");
            myRadar.detections.Add(other.gameObject);
            if (myShip.playerShip == true)
            {
                otherRadar.miniMapIcon.SetActive(true);
            }
        }

    }

}
