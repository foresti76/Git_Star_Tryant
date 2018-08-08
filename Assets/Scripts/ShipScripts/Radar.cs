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
    public Text targetDisplayText;
    public string targetString;
    public int miniMapSizeScaler = 10;

    float timeToRadarLock;
    private Ship myShip;
    Transform miniMapSelectionBox;
    Transform mapSelectionBox;
    int angle = 0;

    Camera minimapCamera;
	// Use this for initialization
	void Start () {
        minimapCamera = GameObject.Find("MiniMapCamera").GetComponent<Camera>();
        radarTrigger = transform.Find("RadarTrigger").GetComponent<SphereCollider>();
        targetDisplayText = GameObject.Find("PlayerTargetText").GetComponent<Text>();
        myShip = GetComponent<Ship>();
        miniMapSelectionBox = GameObject.Find("MiniMapSelectionBox").transform;
        mapSelectionBox = GameObject.Find("SelectionBox").transform;
    }
    private void OnGUI()
    {
        if (target && targetLock)
        {
            targetDisplayText.text = target.name;
        }
    }
    public void UpdateMinimap()
    {
        minimapCamera.orthographicSize = range + miniMapSizeScaler;
        radarTrigger.radius = range;
    }

    private void LateUpdate()
    {
        //if (myShip.playerShip)
        //{
        //    UpdateMinimap();  //this is debug only.
        //}

        if (target == null)
        {
            targetLock = false;
            if(myShip.playerShip == true)
            {
                targetDisplayText.text = "None";
            }
            return;
        }

        if(target && targetLock == false && myShip.playerShip)
        {
            angle += 10;
            miniMapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
            mapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
        }

        if (timeToRadarLock <= Time.time && targetLock == false)
        {
            targetLock = true;
            angle = 0;
            miniMapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
            mapSelectionBox.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
    // commence radar lock
    public void RadarLock()
    {
        targetLock = false;
        if (myShip.playerShip == true)
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
