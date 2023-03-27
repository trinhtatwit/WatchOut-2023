using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

/*
* UI manager which connects ALL game mechanics, such as HP, money, stress...
* and displays it on the UI 
*/
public class UIManager : MonoBehaviour
{
    public Slider slider;
    public TMP_Text money;
    public Image maskIndicator;
    public Image gloveIndicator;
    public Image faceShieldIndicator;
    public Inventory playerInventory;

    //Sets the money label and sets the mask display
    void Start() 
    {
        setMoney(PlayerStats.getMoney());
        if (playerInventory.currentMask)
        {
            maskIndicator.sprite = playerInventory.currentMask.itemImage;
        }
        if (playerInventory.currentGlove)
        {
            gloveIndicator.sprite = playerInventory.currentGlove.itemImage;
        }
        if (playerInventory.currentFaceShield)
        {
            faceShieldIndicator.sprite = playerInventory.currentFaceShield.itemImage;
        }
    }

    //Keeps the players money and active mask updated to display the appropriate information
    void Update() 
    {
        setMoney(PlayerStats.getMoney());
        if (playerInventory.currentMask) {
            maskIndicator.sprite = playerInventory.currentMask.itemImage;
        }

        if(playerInventory.currentGlove) {
            gloveIndicator.sprite = playerInventory.currentGlove.itemImage;
        }

        if(playerInventory.currentFaceShield) {
            faceShieldIndicator.sprite = playerInventory.currentFaceShield.itemImage;
        }

        
    }

   //Sets the player's health bar to the current health
   public void setHealth(float health) 
   {
       slider.value = health;
   }

   //Sets the highest value the player's health bar can go
   public void setMaxHealth() 
   {
       slider.maxValue = 100;
       slider.value = 100;
   }

   //Sets the money label to the players current money
   private void setMoney(float value)
   {
       money.text = "$ " + value.ToString();
   }

}
