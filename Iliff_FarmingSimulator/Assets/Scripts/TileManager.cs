using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile defaultTile;
    [SerializeField] private Tile plowedTile;
    [SerializeField] private Tile seededTile;
    [SerializeField] private Tile wateredTile;
    void Awake() {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin) {
            TileBase tile = interactableMap.GetTile(position);
            if(tile != null && tile.name == "Interactable_Visible") {
                interactableMap.SetTile(position, hiddenInteractableTile);  // Set the visible tiles to hidden
            }
        }
    }
    // Checks if the tile is interactable
    
    public void SetInteracted(Vector3Int position, string toolName) {
        if (toolName == "Seeds") {
            interactableMap.SetTile(position, seededTile);
        }
        if (toolName == "Hoe")
            interactableMap.SetTile(position, plowedTile);
        if (toolName == "WateringCan")
            // Put animation here
            interactableMap.SetTile(position, wateredTile);
        if (toolName == "Harvester")
            // Put animation here
            interactableMap.SetTile(position, defaultTile);
    }

    public string GetTileName(Vector3Int position) {
        if (interactableMap != null) {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null) {
                return tile.name;
            }
        }

        return "";
    }
}
