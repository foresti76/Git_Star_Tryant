using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

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

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        loot = GameObject.Find("Loot").GetComponent<Loot>();
        toolTip = inv.GetComponent<ToolTip>();
        hullSlot = GameObject.Find("Hull Slot");
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

        Debug.Log("Slot type " + slotType);

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
}
