using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Operates the inventory processes such as creating each inventory slot for each item and making sure the right masks are displayed in each scene once they are bought

public class InventoryManager1 : MonoBehaviour
{
    [Header("Inventory Information")]
    public MarketInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TextMeshProUGUI descriptionText;
    public MarketItem currentItem;
    public PlayerHpManager playerHp;
    public bool beginning;

    //Sets the description text to the description of the corresponding mask
    public void SetText(string description)
    {
        descriptionText.text = description;
    }

    //Creates inventory slots and calls the method that adds the masks to the slots
    public void MakeInventorySlots()
    {
        if (playerInventory)
        {

            foreach (Transform child in inventoryPanel.transform) //Will destroy extra slots that dont belong and update UI when item is deleted
            {
                if (child == blankInventorySlot) continue;
                Destroy(child.gameObject);
            }


            foreach (int i in playerInventory.HasList())
            {
                MarketItem item = playerInventory.ItemIdentify(i);
                RectTransform itemSlotRectTransform = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity).GetComponent<RectTransform>();
                itemSlotRectTransform.SetParent(inventoryPanel.transform);
                InventorySlot1 newSlot = itemSlotRectTransform.gameObject.GetComponent<InventorySlot1>();
                if (newSlot)
                {
                    newSlot.SetUp(item, this);
                }
            }

            /*
            foreach(InventoryItem item in playerInventory.GetMaskList()) //Cleaner way to add inventory slots
            {
                RectTransform itemSlotRectTransform = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity).GetComponent<RectTransform>();
                itemSlotRectTransform.SetParent(inventoryPanel.transform);
                InventorySlot newSlot = itemSlotRectTransform.gameObject.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.SetUp(item, this);
                }
            }
            */

        }
    }

    //Makes sure that the inventory box isn't active and calls the method to create the inventory slots 
    void Start()
    {
        inventoryUI.SetActive(false);
        MakeInventorySlots();
        SetText("");
    }

    void Update()
    {
        //if(size > playerInventory.size())
        //{
        //    size = playerInventory.size();
        //    MakeInventorySlots();
        //}
    }

    //Sets the description based on the item
    public void SetupDescription(string newDescriptionString, MarketItem newItem)
    {
        currentItem = newItem;
        SetText(newDescriptionString);

    }
}
