using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    public Equipment equipment;
    public int ammount = 1;
    public int slot;
    public string slotType;

    private Transform originalParent;
    private Vector2 offset;
    private Inventory inv;
    private Loot loot;
    private ToolTip toolTip;
    GameObject hullSlot;
    GameObject engineSlot;
    GameObject generatorSlot;
    GameObject weaponsLayout;
    GameObject subsystemsLayout;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        loot = GameObject.Find("Loot").GetComponent<Loot>();
        toolTip = inv.GetComponent<ToolTip>();
        hullSlot = GameObject.Find("Hull Slot");
        engineSlot = GameObject.Find("Engine Slot");
        generatorSlot = GameObject.Find("Generator Slot");
        weaponsLayout = GameObject.Find("WeaponsLayout");
        subsystemsLayout = GameObject.Find("SubsystemsLayout");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(equipment != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            //originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent.parent);
            this.transform.position = eventData.position - offset;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (equipment != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
         //todo differnatiate between inv, loot and ship customiaztion panel and set correct parent
        if (slotType == "Ship")
        {
            this.transform.SetParent(hullSlot.transform);
            this.transform.position = hullSlot.transform.position;
        }

        if (slotType == "Engine")
        {
            this.transform.SetParent(engineSlot.transform);
            this.transform.position = engineSlot.transform.position;
        }

        if (slotType == "Generator")
        {
            this.transform.SetParent(generatorSlot.transform);
            this.transform.position = generatorSlot.transform.position;
        }

        if (slotType == "Inv")
        {
            this.transform.SetParent(inv.slots[slot].transform);
            this.transform.position = inv.slots[slot].transform.position;
        }

        if (slotType == "Loot")
        {
            this.transform.SetParent(loot.slots[slot].transform);
            this.transform.position = loot.slots[slot].transform.position;
        }

        if (slotType == "Weapon")
        {
            //todo figure out how I want to set this up
            Transform weaponSlot = weaponsLayout.transform.GetChild(slot);
            this.transform.SetParent(weaponSlot);
            this.transform.position = weaponSlot.transform.position;
        }

        if (slotType == "Subsystem")
        {
            //todo figure out how I want to set this up
            Transform subsystemSlot = subsystemsLayout.transform.GetChild(slot);
            this.transform.SetParent(subsystemSlot);
            this.transform.position = subsystemSlot.transform.position;
        }

        //Debug.Log("Slot type " + slotType);

        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.Activate(equipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.Deactivate();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //todo differnatiate between inv, loot and ship customiaztion panel and set correct parent
        if (slotType == "Ship")
        {
            this.transform.SetParent(hullSlot.transform);
            this.transform.position = hullSlot.transform.position;
        }

        if (slotType == "Inv")
        {
            this.transform.SetParent(inv.slots[slot].transform);
            this.transform.position = inv.slots[slot].transform.position;
        }

        if (slotType == "Loot")
        {
            this.transform.SetParent(loot.slots[slot].transform);
            this.transform.position = loot.slots[slot].transform.position;
        }

        if (slotType == "Weapon")
        {
            //todo figure out how I want to set this up
            Transform weaponSlot = weaponsLayout.transform.GetChild(slot);
            this.transform.SetParent(weaponSlot);
            this.transform.position = weaponSlot.transform.position;
        }

        if (slotType == "Subsystem")
        {
            //todo figure out how I want to set this up
            Transform subsystemSlot = subsystemsLayout.transform.GetChild(slot);
            this.transform.SetParent(subsystemSlot);
            this.transform.position = subsystemSlot.transform.position;
        }

        //Debug.Log("Slot type " + slotType);

        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
