using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The object that stores each item in the player inventory

public class InventorySlot1 : MonoBehaviour
{
    [Header("UI Stuff to Change")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Text txt;

    [Header("Variables from the item")]
    public MarketInventory playerInventory1;
    public MarketItem thisItem1;
    public InventoryManager1 thisManager;
    public ItemDatabase database;
    public int count;

    //Creates the inventory slot and put the given item's image into the inventory button
    public void SetUp(MarketItem newItem, InventoryManager1 newManager)
    {
        thisItem1 = newItem;
        thisManager = newManager;
        count = playerInventory1.getCount(thisItem1.id);
        Debug.Log("count: " + count);
        if (thisItem1)
        {
            itemImage.sprite = thisItem1.itemImage;
            txt.text = "" + count;
        }
    }

    //When the inventory item is clicked on it activates it and changes the player's damage rate and active mask label, also displays a short description of the mask
    public void ClickedOn()
    {
        if (thisItem1 == database.gloves)
        {
            thisManager.playerHp.setGloveDefense(thisItem1.defense);
            thisManager.playerHp.setGloveDurability(thisItem1.durability);
            playerInventory1.setGlove(thisItem1);
            //playerInventory.RemoveGlove();
        }
        else if (thisItem1 == database.faceShield)
        {
            thisManager.playerHp.setFaceShieldDefense(thisItem1.defense);
            thisManager.playerHp.setFaceShieldDurability(thisItem1.durability);
            playerInventory1.setFaceShield(thisItem1);
            //playerInventory.RemoveFaceShield();
        }
        else if (thisItem1 == database.handSanitizer)
        {
            thisManager.playerHp.HandSanitizer();
        }
        else //everything else aka masks will trigger this
        {
            thisManager.playerHp.setMaskDefense(thisItem1.defense);
            thisManager.playerHp.setMaskDurability(thisItem1.durability);
            playerInventory1.setMask(thisItem1);
            //playerInventory.RemoveMask();
        }
        playerInventory1.RemoveItem(thisItem1);
        thisManager.MakeInventorySlots();

    }

    public void showDescription()
    {
        thisManager.SetupDescription(thisItem1.itemDescription, thisItem1);
    }
}
