using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

//Manages stress system for when player does activities 
public class PlayerStressScript : MonoBehaviour
{
    public float stress;

    //public static PlayerStressScript instance { get; private set; }
    public Image mask;
    float currentSize;

    // Start is called before the first frame update
    void Start()
    {   
        //Displays stress
        stress = PlayerStats.getStress();
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, stress*2);
        Debug.Log("Width = " + mask.rectTransform.rect.width);

    }

    // Update is called once per frame
    void Update()
    {
        currentSize = mask.rectTransform.rect.width;
    }

    //void Awake()
    //{
    //    instance = this;
    //}

    //Reduces stress and updates display
    public void loseStress(float a)
    {
        stress -= a;
        PlayerStats.setStress(stress);
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize - (2 * a));
    }

    //Increases stress and updates display
    public void gainStress(float a)
    {
        if (stress+a >= 100) 
        {
            stress = 100;
        } 
        else
        {
            stress += a;
        }
        
        PlayerStats.setStress(stress);
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentSize + (2*a));
    }
}
