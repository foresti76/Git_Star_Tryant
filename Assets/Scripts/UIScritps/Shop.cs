using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject shopPanel;
    public GameObject shopSlotPanel;
    public GameObject shopSlot;
    public GameObject shopItem;
    public PlayerRecord playerRecord;

    ItemDatabase equipmentDatbase;
    private int slotAmmount;


    public List<Equipment> shopEquipments = new List<Equipment>();
    public List<GameObject> shopSlots = new List<GameObject>();
    // Use this for initialization
    void Start () {
        playerRecord = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
        equipmentDatbase = GameObject.Find("Inventory").GetComponent<ItemDatabase>();
        slotAmmount = 20;
        for (int i = 0; i < slotAmmount; i++)
        {
            shopEquipments.Add(new Equipment());
            shopSlots.Add(Instantiate(shopSlot));
            shopSlots[i].transform.SetParent(shopSlotPanel.transform, false);
            shopSlots[i].GetComponent<ShopSlot>().id = i;
        }
        AddShopEquipment(7);
        AddShopEquipment(17);
        AddShopEquipment(27);
        AddShopEquipment(37);
        AddShopEquipment(47);
        AddShopEquipment(57);
        AddShopEquipment(67);
        AddShopEquipment(77);
        AddShopEquipment(87);
        AddShopEquipment(97);
        AddShopEquipment(107);
    }

    public void AddShopEquipment(int id)
    {
        Equipment equipmentToAdd = equipmentDatbase.FetchEquipmentByID(id);
        if (equipmentToAdd.Stackable && (CheckIfEquipmentIsInShop(equipmentToAdd) > -1))
        {
            EquipmentData data = shopSlots[CheckIfEquipmentIsInShop(equipmentToAdd)].transform.GetChild(0).GetComponent<EquipmentData>();
            data.ammount++;
            data.transform.GetChild(0).GetComponent<Text>().text = data.ammount.ToString();
            data.transform.GetChild(1).GetComponent<Text>().text = data.equipment.Cost.ToString();
        }
        else
        {
            for (int i = 0; i < shopEquipments.Count; i++)
            {
                if (shopEquipments[i].ID == -1)
                {
                    shopEquipments[i] = equipmentToAdd;
                    GameObject equipmentObject = Instantiate(shopItem);
                    equipmentObject.transform.SetParent(shopSlots[i].transform, false);
                    //InventorySlot invSlot = equipmentObject.transform.parent.GetComponent<InventorySlot>();
                    equipmentObject.transform.localPosition = new Vector2(0, 0);
                    equipmentObject.GetComponent<Image>().sprite = equipmentToAdd.Sprite;
                    equipmentObject.name = equipmentToAdd.Title;
                    EquipmentData data = equipmentObject.transform.GetComponent<EquipmentData>();
                    data.equipment = equipmentToAdd;
                    data.slotType = "Shop";
                    data.ammount++;
                    data.slot = i;
                    data.transform.GetChild(1).GetComponent<Text>().text = data.equipment.Cost.ToString();
                    break;
                }
            }
        }
    }

    int CheckIfEquipmentIsInShop(Equipment equipment)
    {
        for (int i = 0; i < shopEquipments.Count; i++)
        {
            //todo set a variable that can be accessed rather than having to iterate through them all every time.
            if (shopEquipments[i].ID == equipment.ID)
            {
                return i;
            }
        }
        return -1;
    }
}
