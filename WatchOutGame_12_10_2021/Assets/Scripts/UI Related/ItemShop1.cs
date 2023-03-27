using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using TMPro;

//Used for managing item shop menu
public class ItemShop1 : MonoBehaviour
{
    private int[] itemCosts = { 1, 2, 3, 5, 2, 5 }; // Each items cost in the order of: cloth mask, surgical mask, n95 mask, face shield, gloves, vaccine
    public PlayerMoneyScript PlayerMoneyScript;
    public GameObject notEnough;
    public TextMeshProUGUI descriptionText;
    public GameObject description;

    //Cloth Mask UI Assests
    [Header("Cloth Mask Information")]
    public TextMeshProUGUI labelClothMask;
    public GameObject buttonClothMask;

    //Surgical Mask UI Assests
    [Header("Surgical Mask Information")]
    public TextMeshProUGUI labelSurgicalMask;
    public GameObject buttonSurgicalMask;

    //N95 Mask UI Assests
    [Header("N95 Mask Information")]
    public TextMeshProUGUI labelN95Mask;
    public GameObject buttonN95Mask;

    //Face Shield UI Assests
    [Header("Face Shield Information")]
    public TextMeshProUGUI labelFaceShield;
    public GameObject buttonFaceShield;

    //Glove UI Assests
    [Header("Gloves Information")]
    public TextMeshProUGUI labelGloves;
    public GameObject buttonGloves;

    //Vaccine UI Assests
    [Header("Hand Sanitizer Information")]
    public TextMeshProUGUI labelHandSanitizer;
    public GameObject buttonHandSanitizer;

    //Inventory Related Variables
    [Header("Inventory Information")]
    public MarketInventory playerInventory;
    public List<MarketItem> purchasableItems = new List<MarketItem>();

    //Sets each items state appropriately according to what has been purchased
    void Start()
    {

    }

    //All "buy..." methods buy corresponding item
    public void buyClothMask()
    {

        if (PlayerMoneyScript.money >= itemCosts[0])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[0]);
            playerInventory.addMask(purchasableItems[0]);
            ReportPurchase("Cloth Mask");
        }
        else
        {
            tooPoor();
        }


    }

    public void buySurgicalMask()
    {

        if (PlayerMoneyScript.money >= itemCosts[1])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[1]);
            playerInventory.addMask(purchasableItems[1]);
            ReportPurchase("Surgical Mask");
        }
        else
        {
            tooPoor();
        }


    }

    public void buyN95Mask()
    {

        if (PlayerMoneyScript.money >= itemCosts[2])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[2]);
            playerInventory.addMask(purchasableItems[2]);
            ReportPurchase("N95 Mask");
        }
        else
        {
            tooPoor();
        }


    }

    public void buyFaceShield()
    {
        if (PlayerMoneyScript.money >= itemCosts[3])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[3]);
            playerInventory.addMask(purchasableItems[3]);
            ReportPurchase("Face Shield");
        }
        else
        {
            tooPoor();
        }
    }
    public void buyHandSanitizer()
    {
        if (PlayerMoneyScript.money >= itemCosts[5])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[5]);
            playerInventory.addMask(purchasableItems[5]);
            ReportPurchase("Hand Sanitizer");
        }
        else
        {
            tooPoor();
        }
    }
    public void buyGloves()
    {
        if (PlayerMoneyScript.money >= itemCosts[4])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[4]);
            playerInventory.addMask(purchasableItems[4]);
            ReportPurchase("Gloves");
        }
        else
        {
            tooPoor();
        }
    }

    public void buyVaccine()
    {
        if (PlayerMoneyScript.money >= itemCosts[5])
        {
            notEnough.SetActive(false);
            PlayerMoneyScript.loseMoney(itemCosts[5]);
            //PlayerStats.boughtVaccine();
            // purchasedVaccine();
            // hideVaccine();
        }
        else
        {
            tooPoor();
        }
    }

    public void purchasedVaccine()
    {
        //buttonVaccine.SetActive(false);
        //labelVaccine.SetText("Purchased!");
    }

    //displays message that player does have enough money to buy item
    void tooPoor()
    {
        notEnough.SetActive(true);
        description.SetActive(false);
    }

    //"show..." & "hide..." displays description for item when hovering over button 
    public void showClothMask()
    {
        descriptionText.text = "Cloth Mask: a mask made from fabric used to cover your face so you can protect yourself from being infected and limit you spreading the virus if infected";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideClothMask()
    {
        description.SetActive(false);
    }

    public void showSurgicalMask()
    {
        descriptionText.text = "Surgical Mask: a layered mask made from non-woven fabric and melt-blown polymer often used by doctors and other medical professionals, is more effective at limiting the spread of viruses and disease than regular cloth mask";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideSurgicalMask()
    {
        description.SetActive(false);
    }

    public void showN95Mask()
    {
        descriptionText.text = "N95 Mask: a mask made from melt-blown polymer forming a fine mesh that can fliter out 95% of particles that pass through. Used by doctors and other medical professionals, also used in industrial sectors to protect workers from small particles. Is more effective at limiting the spread of viruses and disease than surgical mask.";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideN95Mask()
    {
        description.SetActive(false);
    }

    public void showFaceShield()
    {
        descriptionText.text = "Face Shield: a plastic sheet that is attached to a headstrap or helmet to protect your entire face from objects and infectious materials. Used in many sectors/industries including medical primarily to protect against infectious fluids. Only provides minimal protection against diseases and viruses, not nearly as effective by itself than a mask. ";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideFaceShield()
    {
        description.SetActive(false);
    }

    public void showGloves()
    {
        descriptionText.text = "Gloves: a disposable handwear used to protect doctors from spreading dieases between people when coming into contact with them. Can be made from many different types of polymer.  ";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideGloves()
    {
        description.SetActive(false);
    }

    public void showHandSanitizer()
    {
        descriptionText.text = "Hand Sanitizer: An liquid solution used to clean hands and kill germs on said hands.";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideHandSanitizer()
    {
        description.SetActive(false);
    }

    public void showVaccine()
    {
        descriptionText.text = "Vaccine: a substance or material that protects people from catching diseases by making the body create antibodies against the disease, giving a person immunity. How effective the immunity is and how long it lasts depends on many factors, such as what type of vaccine it is.  ";
        description.SetActive(true);
        notEnough.SetActive(false);
    }

    public void hideVaccine()
    {
        description.SetActive(false);
    }


    public void ReportPurchase(string itemName)
    {
        AnalyticsEvent.Custom("Item_Purchased", new Dictionary<string, object>
        {
            { "Item_Bought", itemName },
        });
    }

}
