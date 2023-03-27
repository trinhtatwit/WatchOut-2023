using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHpManager : MonoBehaviour
{
    
    public float hp, maxHP;
    private static bool damage;
    public WinLoseManagement WinLoseScript;
    public UIManager healthBar;

    //important damage calculations
    public float dmgLevel; //the actual damage
    public float baseDamage; //damage without any defenses
    public float gloveDefense; //amount of damage blocked by gloves
    public float maskDefense; //amount of damage blocked by mask
    public float faceShieldDefense; //amount of damage blocked by face shield
    public float totalDefense; //total number of damage being blocked


    public int maskDuraLevel;
    public int gloveDuraLevel;
    public int faceShieldDuraLevel;
    public SliderManager maskDuraSlider;
    public SliderManager gloveDuraSlider;
    public SliderManager faceShieldDuraSlider;

    public Inventory playerInventory;
    public InventoryItem empty;
    public InventoryManager invenManager;

    public UnityEvent loseDurability_event;
    public Text durabilityText;


    // Start is called before the first frame update
    void Start()
    {
        baseDamage = 30;
        maxHP = 100;
        if (!playerInventory.currentMask)
        {
            playerInventory.setMask(empty);
            invenManager.MakeInventorySlots();
            maskDuraLevel = 0;
            maskDefense = 0;
        }
        else 
        {
            maskDefense = playerInventory.currentMask.defense;
            maskDuraLevel = playerInventory.currentMask.durability;
            maskDuraSlider.SetSliderMaxValue(playerInventory.currentMask.durability);
            maskDuraSlider.SetSliderCurrentValue(maskDuraLevel);

        }

        if (!playerInventory.currentGlove)
        {
            playerInventory.setGlove(empty);
            invenManager.MakeInventorySlots();
            gloveDuraLevel = 0;
            gloveDefense = 0;
        }
        else 
        {
            gloveDefense = playerInventory.currentGlove.defense;
            gloveDuraLevel = playerInventory.currentGlove.durability;
            gloveDuraSlider.SetSliderMaxValue(playerInventory.currentGlove.durability);
            gloveDuraSlider.SetSliderCurrentValue(gloveDuraLevel);
        }

        if (!playerInventory.currentFaceShield)
        {
            playerInventory.setFaceShield(empty);
            invenManager.MakeInventorySlots();
            faceShieldDuraLevel = 0;
            faceShieldDefense = 0;
        }
        else 
        {
            faceShieldDefense = playerInventory.currentFaceShield.defense;
            faceShieldDuraLevel = playerInventory.currentFaceShield.durability;
            faceShieldDuraSlider.SetSliderMaxValue(playerInventory.currentFaceShield.durability);
            faceShieldDuraSlider.SetSliderCurrentValue(faceShieldDuraLevel);
        }


        totalDefense = maskDefense + gloveDefense+faceShieldDefense;
        dmgLevel = baseDamage - totalDefense;

        hp = PlayerStats.getHealth();
        Invoke("damageCheck", 1);
        healthBar.setHealth(hp);
    }

    // Update is called once per frame
    void Update()
    {
        //call boolean from PlayerEntersRing.cs
        damage = PlayerEntersRing.takingDamage;
        DurabilityCheck();
        if (hp <= 0)
        {
            print("hp reached 0");
            WinLoseScript.OnPlayerLose();
        }
    }


    public void DurabilityDecrease() //Decreases Durability and is triggered by the Unity Event on Player HP Manager
    {
        if(playerInventory.currentMask != empty)
        {
            maskDuraLevel = maskDuraLevel - 10;
            if(maskDuraLevel <= 0)
            {
                invenManager.MakeInventorySlots();
            }
        }

        if(playerInventory.currentGlove != empty)
        {
            gloveDuraLevel = gloveDuraLevel - 10;
            if(gloveDuraLevel <= 0)
            {
                invenManager.MakeInventorySlots();
            }
        }

        if(playerInventory.currentFaceShield != empty)
        {
            faceShieldDuraLevel = faceShieldDuraLevel - 10;
            if(faceShieldDuraLevel <= 0)
            {
                invenManager.MakeInventorySlots();
            }
        }
    }



    public void DurabilityCheck() //Checks if the durability is below or at 0 and will remove it from the list. It will also update the testing text.
    {
        if(playerInventory.currentMask != MasksDatabase.Instance.empty)
        {
            maskDuraSlider.SetSliderCurrentValue(maskDuraLevel);
            if(maskDuraSlider.gameObject.activeSelf == false)
            {
                maskDuraSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            maskDuraSlider.gameObject.SetActive(false);
        }
        if(maskDuraLevel <= 0)
        {
            playerInventory.currentMask = MasksDatabase.Instance.empty;      
        }

        if(playerInventory.currentGlove != MasksDatabase.Instance.empty)
        {
            gloveDuraSlider.SetSliderCurrentValue(gloveDuraLevel);
            if(gloveDuraSlider.gameObject.activeSelf == false)
            {
                gloveDuraSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            gloveDuraSlider.gameObject.SetActive(false);
        }
        if(gloveDuraLevel <= 0)
        {
            playerInventory.currentGlove = MasksDatabase.Instance.empty;      
        }

        if(playerInventory.currentFaceShield != MasksDatabase.Instance.empty)
        {
            faceShieldDuraSlider.SetSliderCurrentValue(faceShieldDuraLevel);
            if(faceShieldDuraSlider.gameObject.activeSelf == false)
            {
                faceShieldDuraSlider.gameObject.SetActive(true);
            }
        }
        else
        {
            faceShieldDuraSlider.gameObject.SetActive(false);
        }
        if(gloveDuraLevel <= 0)
        {
            playerInventory.currentFaceShield = MasksDatabase.Instance.empty;      
        }
    }



    //checks if the player is currently taking damage
    void damageCheck()
    {
        if (damage == true)
        {
            Invoke("giveDamage", 0);
        }
        Invoke("damageCheck", 1);
    }

    //rolls to determine if player should take damage (70% chance to take damage)
    void giveDamage()
    {
        int damageChance = Random.Range(0, 9);
        if (damageChance >= 0 && damageChance <= 4)
        {
            hp -= dmgLevel;
            healthBar.setHealth(hp);
            PlayerStats.setHealth(hp);
            loseDurability_event.Invoke();
            
        }
    }
/*
    public void setDefense(float def)
    {
        totalDefense = def;
        setDamage(totalDefense);
    }

    public void setDamage(float def) 
    {
        dmgLevel = baseDamage-def;
    }

    public void setDurability(int dura)
    {
        maskDuraLevel = dura;
    }
*/

    public void setGloveDefense(float def)
    {
        gloveDefense = def;
        updateDefenseAndDamage();
    }

    public void setGloveDurability(int dura)
    {
        gloveDuraLevel = dura;
    }

    public void setMaskDefense(float def)
    {
        maskDefense = def;
        updateDefenseAndDamage();
    }

    public void setMaskDurability(int dura)
    {
        maskDuraLevel = dura;
    }

    public void setFaceShieldDefense(float def)
    {
        faceShieldDefense = def;
        updateDefenseAndDamage();
    }

    public void setFaceShieldDurability(int dura)
    {
        faceShieldDuraLevel = dura;
    }

	//heals player by .15 health
	public void sinkHeal() 
	{
		hp += 0.15f;
        healthBar.setHealth(hp);
        PlayerStats.setHealth(hp);
	}
    public void HandSanitizer()
    {
        hp = maxHP;
        healthBar.setHealth(hp);
        PlayerStats.setHealth(hp);
    }
    //returns player health points
    public float healthPoints() 
	{
		return hp;
	}

    public void updateDefenseAndDamage()
    {
        totalDefense = maskDefense + gloveDefense+faceShieldDefense;
        dmgLevel = baseDamage - totalDefense;
    }

}
