using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots_UI : MonoBehaviour
{     
     // Set the item icon and quantity 
   public int slotID;
   public Inventory inventory;
   public Image itemIcon;
   public TextMeshProUGUI quantityText;
   //[SerializeField] private GameObject highlight;

   public void SetItem(Inventory.Slot slot) {
        if(slot != null) {    // If the slot is not empty set the item sprite, color and quantity
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            quantityText.text = slot.count.ToString();
        }
   } 

   public void SetEmpty() {    // If the slot is empty set it to the empty design
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
   }

//    public void SetHighlight(bool on) {
//           highlight.SetActive(on);
//    }
}
