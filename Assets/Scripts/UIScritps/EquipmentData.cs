using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentData : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler {

    public Equipment equipment;
    public int ammount = 1;
    public int cost;
    public int slot;
    public string slotType;
    public Text amountText;
    public Text costText;

    private Transform originalParent;
    private Vector2 offset;
    private Inventory inv;
    private LootPanel loot;
    private Shop shop;
    private ToolTip toolTip;
    GameObject hullSlot;
    GameObject engineSlot;
    GameObject generatorSlot;
    GameObject rcsSlot;
    GameObject radarSlot;
    GameObject shieldSlot;
    GameObject ecmSlot;
    GameObject tractorBeamSlot;
    GameObject weaponsLayout;
    GameObject subsystemsLayout;

    void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        loot = GameObject.Find("LootPanelControl").GetComponent<LootPanel>();
        shop = GameObject.Find("ShopPanelControl").GetComponent<Shop>();

        toolTip = inv.GetComponent<ToolTip>();
        hullSlot = GameObject.Find("Hull Slot");
        engineSlot = GameObject.Find("Engine Slot");
        generatorSlot = GameObject.Find("Generator Slot");
        rcsSlot = GameObject.Find("RCS Slot");
        radarSlot = GameObject.Find("Radar Slot");
        shieldSlot = GameObject.Find("Shield Slot");
        ecmSlot = GameObject.Find("ECM Slot");
        tractorBeamSlot = GameObject.Find("Tractor Beam Slot");
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

        if (slotType == "Shop")
        {
            this.transform.SetParent(shop.shopSlots[slot].transform);
            this.transform.position = shop.shopSlots[slot].transform.position;
        }

        if (slotType == "Engine")
        {
            this.transform.SetParent(engineSlot.transform);
            this.transform.position = engineSlot.transform.position;
        }

        if (slotType == "RCS")
        {
            this.transform.SetParent(rcsSlot.transform);
            this.transform.position = rcsSlot.transform.position;
        }

        if (slotType == "ECM")
        {
            this.transform.SetParent(ecmSlot.transform);
            this.transform.position = ecmSlot.transform.position;
        }

        if (slotType == "Radar")
        {
            this.transform.SetParent(radarSlot.transform);
            this.transform.position = radarSlot.transform.position;
        }

        if (slotType == "Shield")
        {
            this.transform.SetParent(shieldSlot.transform);
            this.transform.position = shieldSlot.transform.position;
        }

        if (slotType == "TractorBeam")
        {
            this.transform.SetParent(tractorBeamSlot.transform);
            this.transform.position = tractorBeamSlot.transform.position;
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

        if (slotType == "Shop")
        {
            this.transform.SetParent(shop.shopSlots[slot].transform);
            this.transform.position = shop.shopSlots[slot].transform.position;
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
