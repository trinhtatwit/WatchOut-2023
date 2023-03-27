using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Script for the objects in the players inventory, keeps track of the items name, description, image, and what value it changes the damage the player takes

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Items")]
public class MarketItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public float defense;
    public int durability;
    public int id;
}
