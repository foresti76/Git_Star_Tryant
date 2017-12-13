using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {

    public int hull;
    public int shield;
    public int engine;
    public int ecm;
    public int radar;
    public int rcs;
    public int tractorbeam;
    public int generator;
    public List<int> weaponList;
    public List<int> subsystemList;

    EngineSlot engineSlot;
    HullSlot hullSlot;
    GeneratorSlot generatorSlot;
    RCSSlot rcsSlot;
    RadarSlot radarSlot;
    ShieldSlot shieldSlot;
    ECMSlot ecmSlot;
    TractorBeamSlot tractorBeamSlot;
    GameObject weaponsLayout;
    GameObject subsystemsLayout;



    private void Start()
    {
        hullSlot = GameObject.Find("Hull Slot").GetComponent<HullSlot>();
        generatorSlot = GameObject.Find("Generator Slot").GetComponent<GeneratorSlot>();
        rcsSlot = GameObject.Find("RCS Slot").GetComponent<RCSSlot>();
        radarSlot = GameObject.Find("Radar Slot").GetComponent<RadarSlot>();
        shieldSlot = GameObject.Find("Shield Slot").GetComponent<ShieldSlot>();
        ecmSlot = GameObject.Find("ECM Slot").GetComponent<ECMSlot>();
        tractorBeamSlot = GameObject.Find("Tractor Beam Slot").GetComponent<TractorBeamSlot>();
        weaponsLayout = GameObject.Find("WeaponsLayout");
        subsystemsLayout = GameObject.Find("SubsystemsLayout");
        engineSlot = GameObject.Find("Engine Slot").GetComponent<EngineSlot>();
        SaveData saveData = GameObject.Find("SaveLoad").GetComponent<SaveData>();
        saveData.Load();
        BuildShip();
    }


    public void BuildShip()
    {
        hullSlot.UpdateHull(hull);
        engineSlot.UpdateEngine(engine);
        shieldSlot.UpdateShield(shield);
        radarSlot.UpdateRadar(radar);
        rcsSlot.UpdateRCS(rcs);
        tractorBeamSlot.UpdateTractorBeam(tractorbeam);
        generatorSlot.UpdateGenerator(generator);
        ecmSlot.UpdateECM(ecm);

        WeaponSlot[] weaponSlots = weaponsLayout.GetComponentsInChildren<WeaponSlot>();

        for (int i = 0; i < weaponList.Count; i++)
        {
            weaponSlots[i].UpdateWeapon(weaponList[i]);
        }

        //todo add in subsystem inialization

    }

    public void UpdateWeaponList()
    {
        WeaponSlot[] weaponSlots = weaponsLayout.GetComponentsInChildren<WeaponSlot>();

        weaponList.Clear();

        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            weaponList.Add(weaponSlot.GetComponentInChildren<EquipmentData>().equipment.ID);
        }
    }
    
}
