using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDetection : MonoBehaviour {
    public Radar myRadar;

	// Use this for initialization
	void Start () {
        myRadar = transform.GetComponentInParent<Radar>();
	}


    void OnTriggerEnter(Collider other)
    {
        ECM otherECM = other.gameObject.GetComponent<ECM>();
        if (otherECM && Vector3.Distance(transform.position, other.transform.position) <= myRadar.range - otherECM.detectionDefense && other.tag != "Level")
        {
            Radar otherRadar = other.GetComponent<Radar>();
            otherRadar.miniMapIcon.SetActive(true);
            myRadar.detections.Add(other.gameObject);
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
        //todo remove things from my list and hide thier minimap icon if they are out of range.
        if (myRadar.detections.Contains(other.gameObject))
        {
            return;
        }
        ECM otherECM = other.GetComponent<ECM>();
        if (otherECM && Vector3.Distance(transform.position, other.transform.position) <= myRadar.range - otherECM.detectionDefense && other.tag != "Level")
        {
            Radar otherRadar = other.GetComponent<Radar>();
            otherRadar.miniMapIcon.SetActive(true);
            myRadar.detections.Add(other.gameObject);
        }
    }

}
