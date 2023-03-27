using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//The object that stores each item in the player inventory

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to Change")]
    [SerializeField] private Image itemImage;
    [SerializeField] private Text txt;

    [Header("Variables from the item")]
    public Inventory playerInventory;
    public InventoryItem thisItem;
    public InventoryManager thisManager;
    public MasksDatabase database;
    public int count;
    
    //Creates the inventory slot and put the given item's image into the inventory button
    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        count = playerInventory.getCount(thisItem.id);
        Debug.Log("count: "+count);
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            txt.text = ""+count;
        }
    }

    //When the inventory item is clicked on it activates it and changes the player's damage rate and active mask label, also displays a short description of the mask
    public void ClickedOn() 
    {
        if (thisItem == database.gloves) 
        {
            thisManager.playerHp.setGloveDefense(thisItem.defense);
            thisManager.playerHp.setGloveDurability(thisItem.durability);
            playerInventory.setGlove(thisItem);
            //playerInventory.RemoveGlove();
        } 
        else if (thisItem == database.faceShield)
        {
            thisManager.playerHp.setFaceShieldDefense(thisItem.defense);
            thisManager.playerHp.setFaceShieldDurability(thisItem.durability);
            playerInventory.setFaceShield(thisItem);
            //playerInventory.RemoveFaceShield();
        }
        else if (thisItem == database.handSanitizer)
        {
            thisManager.playerHp.HandSanitizer();
        }
        else //everything else aka masks will trigger this
        {
            thisManager.playerHp.setMaskDefense(thisItem.defense);
            thisManager.playerHp.setMaskDurability(thisItem.durability);
            playerInventory.setMask(thisItem);
            //playerInventory.RemoveMask();
        }
        playerInventory.RemoveItem(thisItem);
        thisManager.MakeInventorySlots();

    }

    public void showDescription()
    {
        thisManager.SetupDescription(thisItem.itemDescription, thisItem);
    }
}
