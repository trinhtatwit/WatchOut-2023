using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The inventory object, that stores inventory slots and displays the items so that the player can change what mask they are currently using

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory/Player Inventory")]
[System.Serializable]
public class Inventory : ScriptableObject {
    public List<int> counts = new List<int>();//0=clothMask; 1=startingMask; 2=faceShield; 3=gloves; 4=n95Mask; 5=surgicalMask; 6=handSanitizer;
    public List<InventoryItem> myMasks = new List<InventoryItem>();
    public InventoryItem currentMask;
    public InventoryItem currentGlove;
    public InventoryItem currentFaceShield;
    public MasksDatabase maskDB;

    //At the start of the scene it makes sure the list of masks is empty and that there is not current mask active
    void Start() 
    {
        
    }

    void Awake()
    {
        Debug.Log("Counts:"+counts.Count);
        for(int i = 0; i < counts.Count; i++)
        {
            counts[i] = 0;
        }


    }

    public void clear()
    {
        for(int i = 0; i < counts.Count;i++)
        {
            counts[i] = 0;
        }
        myMasks = new List<InventoryItem>();
        currentMask = null;
        currentGlove = null;
        currentFaceShield = null;

    }

    //Method for adding masks to the players inventory
    public void addMask(InventoryItem newItem) 
    {
        counts[newItem.id]++;

        myMasks.Add(newItem);
    }

    //removes the mask that currently being worn
    public void RemoveMask()
    {
        myMasks.Remove(currentMask);
    }

    //removes the glove that is currently being worn
    public void RemoveGlove()
    {
        myMasks.Remove(currentGlove);
    }

    public void RemoveFaceShield()
    {
        myMasks.Remove(currentFaceShield);
    }

    public void RemoveItem(InventoryItem item) 
    {
        counts[item.id]--;
        myMasks.Remove(item);
        
    }

    //iterates through every "has" method to return a list of every different type of item is in the inventory
    public List<int> HasList()
    {
        List<int> result = new List<int>();
        if(hasClothMask())
        {
            result.Add(0);
        }
        if(hasDefaultMask())
        {
            result.Add(1);
        }
        if(hasFaceShield())
        {
            result.Add(2);
        }
        if(hasGloves())
        {
            result.Add(3);
        }
        if(hasN95Mask())
        {
            result.Add(4);
        }
        if(hasSurgicalMask())
        {
            result.Add(5);
        }
        if(hasHandSanitizer())
        {
            result.Add(6);
        }
        return result;
    }

    public InventoryItem ItemIdentify(int id)
    {

        if(id == 0)
        {
            return maskDB.clothMask;
        }
        else if(id == 1)
        {
            return maskDB.startingMask;
        }
        else if(id == 2)
        {
            return maskDB.faceShield;
        }
        else if(id == 3)
        {
            return maskDB.gloves;
        }
        else if(id == 4)
        {
            return maskDB.n95Mask;
        }
        else if(id == 5)
        {
            return maskDB.surgicalMask;
        }
        else 
        {
            return maskDB.handSanitizer;
        }
        
    }

    public int getCount(int item_id)
    {
        return counts[item_id];
    }

    //Method for setting the player's currently equiped mask
    public void setMask(InventoryItem newItem) 
    {
        currentMask = newItem;
    }

    public void setGlove(InventoryItem newItem)
    {
        currentGlove = newItem;
    }

    public void setFaceShield(InventoryItem newItem)
    {
        currentFaceShield = newItem;
    }

    public bool hasMaskOn()
    {
        return currentMask != null;
    }

    public List<InventoryItem> GetMaskList()
    {
        return myMasks;
    }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Bool methods to check if they own a certain item
    public bool hasClothMask() 
    {   
        //previous version that checked the actual list of masks
        /*
        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("Cloth Mask"))
            {
                return true;
            }
        }
        return false;
        */

        //new version that checks counts
        return counts[0] > 0;
    }
    
    public bool hasDefaultMask() 
    {
        //previous version that checked the actual list of masks
        /*
        for (int i = 0; i < myMasks.Count; i++) 
        {
            if (myMasks[i].itemName.Equals("Default Mask")) 
            {
                return true;
            }        
        }
        return false;
        */

        //new version that checks counts
        return counts[1] > 0;
    }

    public bool hasFaceShield()
    {
        //previous version that checked the actual list of masks
        /*

        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("Face Shield"))
            {
                return true;
            }
        }
        return false;
        */
        
        //new version that checks counts
        return counts[2] > 0;
    }

    public bool hasGloves()
    {
        //previous version that checked the actual list of masks
        /*
        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("Gloves"))
            {
                return true;
            }
        }
        return false;
        */

        //new version that checks counts
        return counts[3] > 0;
    }

    public bool hasN95Mask()
    {
        //previous version that checked the actual list of masks
        /*
        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("N95 Mask"))
            {
                return true;
            }
        }
        return false;
        */
        //new version that checks counts
        return counts[4] > 0;
    }


    public bool hasSurgicalMask()
    {
        //previous version that checked the actual list of masks
        /*

        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("Surgical Mask"))
            {
                return true;
            }
        }
        return false;
        */
        //new version that checks counts
        return counts[5] > 0;
    }

    public bool hasHandSanitizer()
    {
        //previous version that checked the actual list of masks
        /*

        for (int i = 0; i < myMasks.Count; i++)
        {
            if (myMasks[i].itemName.Equals("Hand Sanitizer"))
            {
                return true;
            }
        }
        return false;
        */

        //new version that checks counts
        return counts[6] > 0;
    }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //returns the amount of items that the user has
    public int size()
    {
        return myMasks.Count;
    }
}
