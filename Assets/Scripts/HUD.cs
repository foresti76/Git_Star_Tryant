using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Slider playerShieldHUD;
    public Slider playerHullHUD;
    public Slider playerPowerHUD;
    public GameObject ammoPrefab;
    public Transform ammoLayout;

    GameObject player;
    Hull hull;
    Generator generator;
    Shield shield;
    public List<GameObject> ammoDisplays = new List<GameObject>(); 
    float shieldDisplayValue;
    float hullDisplayValue;
    float powerDisplayValue;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        hull = player.GetComponent<Hull>();
        generator = player.GetComponent<Generator>();
        shield = player.GetComponentInChildren<Shield>();
        HideAmmoDisplay();
	}
	
	// Update is called once per frame
	void Update () {
        playerHullHUD.value = hull.curHull / hull.maxHull;
        playerPowerHUD.value = generator.currentPower / generator.maxPower;
        playerShieldHUD.value = shield.currentShield / shield.maxShield;
	}

    public void CreateWeaponAmmoDisplayElements()
    {
        foreach(GameObject ammoElement in ammoDisplays)
        {
            Destroy(ammoElement);
        }
        ammoDisplays.Clear();

        WeaponController[] tempWeaponControllerArray = player.GetComponentsInChildren<WeaponController>();
        foreach (WeaponController weaponController in tempWeaponControllerArray)
        {
            GameObject ammoElement = Instantiate(ammoPrefab, ammoLayout);
            ammoDisplays.Add(ammoElement);
            ammoElement.transform.GetChild(2).GetComponent<Text>().text = weaponController.weaponName;
            ammoElement.transform.GetChild(3).GetComponent<Text>().text = (weaponController.currentAmmo+ " / " + weaponController.maxAmmo).ToString();
        }
    }

    public void UpdateAmmoDisplay(string weaponName, int id, int ammoAmmount, int maxAmmo)
    {
        float am = ammoAmmount;
        float ma = maxAmmo;
        ammoDisplays[id].GetComponent<Slider>().value = am / ma;
        ammoDisplays[id].transform.GetChild(2).GetComponent<Text>().text = weaponName;
        ammoDisplays[id].transform.GetChild(3).GetComponent<Text>().text = (ammoAmmount + " / " + maxAmmo).ToString();
        if(maxAmmo == 1)
        {
            ammoDisplays[id].transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            ammoDisplays[id].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    public void HideAmmoDisplay()
    {
        ammoLayout.gameObject.SetActive(false);
    }

    public void ShowAmmoDisplay()
    {
        ammoLayout.gameObject.SetActive(true);
    }
}
