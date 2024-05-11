using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();
    [Header("Basket")]
    public Inventory basket;
    public int basketSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake() {
        // basket = null;
        // toolbar = null;
        basket = new Inventory(basketSlotCount);
        toolbar = new Inventory(toolbarSlotCount);

        inventoryByName.Add("Basket", basket);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string inventoryName, Item item) {
        if (inventoryByName.ContainsKey(inventoryName)) {
            inventoryByName[inventoryName].Add(item);
        }
    }
    public Inventory GetInventoryByName(string inventoryName) {
        if (inventoryByName.ContainsKey(inventoryName)) {
            return inventoryByName[inventoryName];
        }
        return null;
    }
}