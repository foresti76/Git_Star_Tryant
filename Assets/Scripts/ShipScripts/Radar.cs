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
    private Text targetName;

    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        radarTrigger = transform.Find("RadarTrigger").GetComponent<SphereCollider>();
        targetName = GameObject.Find("PlayerTargetText").GetComponent<Text>();
	}
	
    public void UpdateMinimap()
    {
        minimapCamera.orthographicSize = range;
        radarTrigger.radius = range;
    }

    private void LateUpdate()
    {
        if (!target)
        {
            targetLock = false;
            targetName.text = "None";
            return;
        }

        if (timeToRadarLock <= Time.time && targetLock == false)
        {
            targetLock = true;
            targetName.text = target.gameObject.name;
            Debug.Log("Target locked");
        }
    }
    // commence radar lock
    public void RadarLock()
    {
        targetLock = false;
        if (target.GetComponent<ECM>())
        { 
            timeToRadarLock = Time.time + target.GetComponent<ECM>().lockDefense;
        }
        else
        {
            targetLock = true;
            targetName.text = target.gameObject.name;
        }
    }
}
