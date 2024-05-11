using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    // Update is called once per frame
    public string inventoryName;
    //public Player player;                // Gets the player that the inventory is attached to
    public List<Slots_UI> slots = new List<Slots_UI>();   // Gets the slots in the inventory 
    [SerializeField] private Canvas canvas;

    //private bool dragSingle;
    private Inventory inventory;

    public void Awake() {
        canvas = FindObjectOfType<Canvas>();
    }

    public void Start() {
        inventory = GameManager.instance.player.inventory.GetInventoryByName(inventoryName);
        SetUpSlots();
        Refresh();
    }

    // public void Update() {
    //     if(Input.GetKey(KeyCode.Tab)) {
    //         ToggleInventory();
    //     }
    // }

    public void Refresh() {
        if (slots.Count == inventory.slots.Count) {
            for (int i = 0; i < slots.Count; i++) {
                if(inventory.slots[i].itemName != "") {    // If there are items in the slots 
                    slots[i].SetItem(inventory.slots[i]);  // Set items (UI) in the slots
                }
                else {
                    slots[i].SetEmpty();   // Otherwise set the slots to the empty design
                }
            }
        }
    }

    public void Remove() {
        // Get the item to drop
        Item itemToDrop = GameManager.instance.itemManager.GetItemByName(inventory.slots[UIManager.draggedSlot.slotID].itemName);
        
        // If the item exists, drop the item, and refresh the inventory design
        if (itemToDrop != null) {
            if (UIManager.dragSingle) {
                GameManager.instance.player.DropItem(itemToDrop);
                inventory.Remove(UIManager.draggedSlot.slotID);
            }
            else {
                GameManager.instance.player.DropItem(itemToDrop, inventory.slots[UIManager.draggedSlot.slotID].count);
                inventory.Remove(UIManager.draggedSlot.slotID, inventory.slots[UIManager.draggedSlot.slotID].count);
            }
            Refresh();
        }

        UIManager.draggedSlot = null;
    }

    public void SlotBeginDrag(Slots_UI slot) {
        UIManager.draggedSlot = slot;
        UIManager.draggedIcon = Instantiate(UIManager.draggedSlot.itemIcon);
        UIManager.draggedIcon.transform.SetParent(canvas.transform);
        UIManager.draggedIcon.raycastTarget = false;
        UIManager.draggedIcon.rectTransform.sizeDelta = new Vector2(50, 50);

        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        //Debug.Log(draggedSlot.name);
    }

    public void SlotDrag() {
        MoveToMousePosition(UIManager.draggedIcon.gameObject);
        //Debug.Log("Dragging: " + draggedSlot.name);
    }

    public void SlotEndDrag() {
        Destroy(UIManager.draggedIcon.gameObject);
        //Debug.Log("Done" + draggedIcon.name);
        UIManager.draggedIcon = null;
    }

    public void SlotDrop(Slots_UI slot) {
        if (UIManager.dragSingle) {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotID, slot.slotID, slot.inventory);
        }
        else {
            UIManager.draggedSlot.inventory.MoveSlot(UIManager.draggedSlot.slotID, slot.slotID, slot.inventory,
            UIManager.draggedSlot.inventory.slots[UIManager.draggedSlot.slotID].count);
        }

        GameManager.instance.uiManager.RefreshAll(inventoryName);
        //Debug.Log("Dropped" + draggedSlot.name + " : " + slot.name);
    }

    private void MoveToMousePosition(GameObject toMove) {
        if (canvas != null) {
            Vector2 position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);
            
            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }

    void SetUpSlots() {
        int counter = 0;

        foreach(Slots_UI slot in slots) {
            slot.slotID = counter;
            counter++;
            slot.inventory = inventory;
        }
    }
}
