using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaScreenController : MonoBehaviour
{
    public List<Toggle> shipSelect = new List<Toggle>();
    public StarBase currentStarBase;
    public GameObject shipSelectiontoggles;
    public GameObject arenaStartButton;
    public ShipSpawner shipSpawner;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in shipSelectiontoggles.transform)
        {
            shipSelect.Add(child.GetComponent<Toggle>());
        }
    }

    // Update is called once per frame

    public void InitalizeShipSelect()
    {
        int i = 0;
        Debug.Log(i);
        foreach ( Toggle toggle in shipSelect)
        {
            string title = currentStarBase.arenaShips[i].Title;
            toggle.GetComponentInChildren<Text>().text = title;
            toggle.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + title);
            i++;
        }
    }
}
