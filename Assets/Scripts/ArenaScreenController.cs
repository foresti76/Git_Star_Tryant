using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaScreenController : MonoBehaviour
{
    public List<Toggle> shipSelect = new List<Toggle>();
    public StarBase currentStarBase;
    public ToggleGroup shipSelectiontoggles;
    public GameObject arenaStartButton;
    public ShipSpawner shipSpawner;
    public ArenaManager arenaManager;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in shipSelectiontoggles.transform)
        {
            shipSelect.Add(child.GetComponent<Toggle>());
        }
    }

    private void Update()
    {
        if(shipSelectiontoggles.AnyTogglesOn())
        {
            arenaStartButton.SetActive(true);
        }
        else
        {
            arenaStartButton.SetActive(false);
        }
    }

    // Update is called once per frame

    public void InitalizeShipSelect()
    {
        int i = 0;
        foreach ( Toggle toggle in shipSelect)
        {
            string title = currentStarBase.arenaShips[i].Title;
            toggle.GetComponentInChildren<Text>().text = title;
            toggle.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Equipment/" + title);
            i++;
        }
    }

    public void SetArenaShip()
    {
        for (int j = 0; j < shipSelect.Count; j++)
        {
            if (shipSelect[j].isOn)
            {
                arenaManager.arenaShipID = currentStarBase.arenaShips[j].ID;
            }
        }  
    }
}
