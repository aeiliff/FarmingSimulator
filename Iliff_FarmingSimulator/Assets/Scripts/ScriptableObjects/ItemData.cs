using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores Item Data, so it is easier to keep track off and access
[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]

public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
}
