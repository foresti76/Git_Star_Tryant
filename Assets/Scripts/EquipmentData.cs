using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {

    public Equipment equipment;
    public int ammount = 1;
    public int slot;

    private Transform originalParent;
    private Vector2 offset;
    private Inventory inv;
    private ToolTip toolTip;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        toolTip = inv.GetComponent<ToolTip>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(equipment != null)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            originalParent = this.transform.parent;
            this.transform.SetParent(this.transform.parent.parent);
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
        //todo differnatiate between inv and loot and set correct parent
       this.transform.SetParent(inv.slots[slot].transform);
        this.transform.position = inv.slots[slot].transform.position;
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
