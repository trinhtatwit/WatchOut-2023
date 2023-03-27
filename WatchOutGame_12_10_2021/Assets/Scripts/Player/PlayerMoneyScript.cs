using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

//Manages money player has
public class PlayerMoneyScript : MonoBehaviour
{
    public float money;
    public TextMeshProUGUI currentMoney;

    // Start is called before the first frame update
    void Start()
    {   
        //Displays money
        money = PlayerStats.getMoney();
        currentMoney.SetText (" $: " + money.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Reduces money player has and updates display
    public void loseMoney(float a)
    {
        money -= a;
        PlayerStats.setMoney(money); 
        currentMoney.SetText (" $: " + money.ToString());
    }

    //Increases money player has and updates display
    public void gainMoney(float a)
    {
        money += a;
        PlayerStats.setMoney(money); 
        currentMoney.SetText (" $: " + money.ToString());
    }

}
