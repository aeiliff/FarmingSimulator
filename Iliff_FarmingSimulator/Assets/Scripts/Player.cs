using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; // include so we can load new scenes
using UnityEngine;


public class Player : MonoBehaviour
{
	public GameObject spawnPrefab;
	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	private float savedTime;
	public Cow cow;
	private float secondsBetweenSpawning;
	public InventoryManager inventory;
	private Animator animator;
	private TileManager tileManager;
	[HideInInspector]
    	public bool playerCanMove = true;

	private void Start() {
		tileManager = GameManager.instance.tileManager;
		animator = gameObject.GetComponentInChildren<Animator>();
		cow = GameObject.FindGameObjectWithTag("Cow").GetComponent<Cow>();

		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	private void Awake() {
		inventory = GetComponent<InventoryManager>();
	}

	// Press space key to mow the field
	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (tileManager != null) {
				Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
				
				string tileName = tileManager.GetTileName(position);

				if (!string.IsNullOrWhiteSpace(tileName)) {
					Debug.Log("Tilename" + tileName);
					if (tileName == "Interactable" && inventory.toolbar.selectedSlot.itemName == "Hoe") {
						tileManager.SetInteracted(position, "Hoe");
						animator.SetTrigger("Plowing");
					}
					//tileName = tileManager.GetTileName(position);
					if (tileName == "Plowed" && inventory.toolbar.selectedSlot.itemName == "Seeds") {
						tileManager.SetInteracted(position, "Seeds");
						//animator.SetTrigger("Plowing");
					}

					if (tileName == "Seeded" && inventory.toolbar.selectedSlot.itemName == "WateringCan") {
						tileManager.SetInteracted(position, "WateringCan");
						//animator.SetTrigger("Plowing");
					}

					if (tileName == "Watered" && inventory.toolbar.selectedSlot.itemName == "Harvester") {
						tileManager.SetInteracted(position, "Harvester");
						MakeThingToSpawn();
					}
				}
			}
		}

		if(Input.GetKeyDown(KeyCode.M) && inventory.toolbar.selectedSlot.itemName == "Medicine") {
			if (cow != null) {
				Debug.Log("Curing Cow");
				cow.CureCow();
			}
		}
	}

	// Drop the item out of the inventory, drop it will force, so you don't automatically pick it back up
	public void DropItem(Item item) {
		Vector2 spawnLocation = transform.position;
		Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

		Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, 
		Quaternion.identity);

		droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
	}

	public void DropItem(Item item, int numToDrop) {
		for(int i = 0; i < numToDrop; i++) {
			DropItem(item);
		}
	}
	
	void MakeThingToSpawn()
	{
		Vector2 spawnLocation = transform.position;
		//Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

		GameObject clone = Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);//+ spawnOffset, 
		//Quaternion.identity);

		//clone.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
		// create a new gameObject
		//GameObject clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;
	}
}
