using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar_UI : MonoBehaviour
{
    // Start is called before the first frame update
    // Allows for private variable to be accessed in unity
    [SerializeField] private List<Slots_UI> toolbarSlots = new List<Slots_UI>();
    private Slots_UI selectedSlot;

    private void Start() {
        SelectSlot(0);
    }

    private void Update() {
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(Slots_UI slot) {
        SelectSlot(slot.slotID);
    }
    public void SelectSlot(int index) {
        if (toolbarSlots.Count == 9) {
            // if (selectedSlot != null) {
            //     selectedSlot.SetHighlight(false);
            // }
            selectedSlot = toolbarSlots[index];
            Debug.Log("SelectedSlot Name: " + selectedSlot.name);
            //selectedSlot.SetHighlight(true);

            GameManager.instance.player.inventory.toolbar.SelectSlot(index);
        }
    }

    // Check if corresponding number to the inventory has been pressed, and select that slot
    private void CheckAlphaNumericKeys() {
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SelectSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SelectSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SelectSlot(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8)) {
            SelectSlot(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SelectSlot(8);
        }
    }
}
