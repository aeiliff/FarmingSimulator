using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();
    public List<Inventory_UI> inventoryUI;

    public static Slots_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;
    public GameObject inventoryPanel;

    private void Awake() {
        Initialize();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B)) {
            ToggleInventoryUI();
        }
        if(Input.GetKey(KeyCode.LeftShift))  // If tab is pressed toggle the inventory open
            dragSingle = true;
        else
            dragSingle = false;
    }

    public void ToggleInventoryUI() {
        if (inventoryPanel != null) {
            if(!inventoryPanel.activeSelf) {
                inventoryPanel.SetActive(true);
                RefreshInventoryUI("Basket");
            }
            else {
                inventoryPanel.SetActive(false);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName) {
        if (inventoryUIByName.ContainsKey(inventoryName)) {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll(string inventoryName) {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName) {
            keyValuePair.Value.Refresh();
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName) {
        if (inventoryUIByName.ContainsKey(inventoryName)) {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("There is no inventory ui for " + inventoryName);
        return null;
    }

    void Initialize() {
        foreach(Inventory_UI ui in inventoryUI) {
            if(!inventoryUIByName.ContainsKey(ui.inventoryName)) {
                inventoryUIByName.Add(ui.inventoryName, ui);
            }
        }
    }
}
