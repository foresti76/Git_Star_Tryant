using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRecord : MonoBehaviour {
    //GameObject playership;
    public int playerMoney;
    
    Text moneyDisplay;
	// Use this for initialization
	void Start () {
        //playerShip = GameObject.FindGameObjectWithTag("Player");
         moneyDisplay = GameObject.Find("Money").GetComponent<Text>();
        UpdateMoneyDisplay(playerMoney);
	}
	
	// Update is called once per frame
	//void Update () {
	//	if(previousAmount != playerMoney)
 //       {
 //           previousAmount = playerMoney;
 //           UpdateMoneyDisplay(playerMoney);
 //       }

 //       //debug
 //       if (Input.GetKeyDown("p"))
 //       {
 //           playerMoney += 10;
 //       }

 //       if (Input.GetKeyDown("o"))
 //       {
 //           playerMoney -= 10;
 //       }
 //   }
    public void GiveMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyDisplay(playerMoney);
    }

    public void SpendMoney(int amount)
    {
        playerMoney -= amount;
        UpdateMoneyDisplay(playerMoney);
    }

    public bool CanAfford(int amount)
    {
        if(playerMoney >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateMoneyDisplay(int amount)
    {
        moneyDisplay.text = amount.ToString();
    }
}
