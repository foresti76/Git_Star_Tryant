using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour {

    public int range;
    public int level;
    public int targetingRange;
    public int targetingSpeed;
    public int energyCost;
    public int signature;
    public SphereCollider radarTrigger;
    public GameObject miniMapIcon;
    public List<GameObject> detections;
    public GameObject target;
    public bool targetLock = false;

    float timeToRadarLock;
    public Text targetDisplayText;
    public string targetString;
    private Ship myShip;

    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        radarTrigger = transform.Find("RadarTrigger").GetComponent<SphereCollider>();
        targetDisplayText = GameObject.Find("PlayerTargetText").GetComponent<Text>();
        myShip = GetComponent<Ship>();
	}
	
    public void UpdateMinimap()
    {
        minimapCamera.orthographicSize = range;
        radarTrigger.radius = range;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            targetLock = false;
            if(myShip.PlayerShip == true)
            {
                targetDisplayText.text = "None";
            }

            return;
        }

        if (timeToRadarLock <= Time.time && targetLock == false)
        {
            targetLock = true;
            Debug.Log(target.gameObject.name + " locked");
        }
    }
    // commence radar lock
    public void RadarLock()
    {
        targetLock = false;
        if (myShip.PlayerShip == true)
        {
            targetString = target.ToString();
            targetDisplayText.text = targetString;
        }
        if (target.GetComponent<ECM>())
        { 
            timeToRadarLock = Time.time + target.GetComponent<ECM>().lockDefense;
        }
        else
        {
            targetLock = true;
        }
    }
}
