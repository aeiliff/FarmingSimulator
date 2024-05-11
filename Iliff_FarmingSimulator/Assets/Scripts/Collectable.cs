using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
	public int seedValue = 1;
	public bool taken = false;
	public GameObject explosion;

	public Rigidbody2D rb2d;

	// Get the current rigidbody state
	private void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// if the player touches the seeds, it has not already been taken, and the player can move (not dead or victory)
	// then take the seeds
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.tag == "Player" ) && (!taken) && (other.gameObject.GetComponent<Player>().playerCanMove))
		{
			// mark as taken so doesn't get taken multiple times
			taken=true;
			Player player = other.GetComponent<Player>(); // Get the player
			if(player) {
				Item item = GetComponent<Item>();  // Get the item

				if (item != null) {
					player.inventory.Add("Basket", item); // Add item to inventory
					Destroy(this.gameObject);   // Destroy the item
				}
			}
		}
	}

}
