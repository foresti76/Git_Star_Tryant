using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {

    private Equipment equipment;
    private string dataString;
    public GameObject toolTip;

	// Use this for initialization
	void Start () {
        toolTip = GameObject.Find("Tooltip");
        toolTip.SetActive(false);
    }

    void Update()
    {
        if(toolTip.activeSelf == true)
        {
            toolTip.transform.position = Input.mousePosition;
        }
    }
	
    public void Activate(Equipment equipment)
    {
        this.equipment = equipment;
        toolTip.SetActive(true);
        ConstructDataString();
    }

    public void Deactivate()
    {
        toolTip.SetActive(false);
       // this.equipment = null;
    }

    public void ConstructDataString()
    {
        //todo set up different string constructor based on equipment type
        dataString= "<b>" + equipment.Title + "</b>\n\n" + equipment.Description + "";

        toolTip.transform.GetChild(0).GetComponent<Text>().text = dataString;
    }
}
