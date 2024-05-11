using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace so can reference UI elements
using UnityEngine.SceneManagement; // include so we can manipulate SceneManager
using TMPro;

public class GameManager : MonoBehaviour
{
    // Gets the item manager and the tile manager 
    public static GameManager instance;
    public ItemManager itemManager;
    public TileManager tileManager;
    public UIManager uiManager;
    public Player player;
    public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;
	
	public TMP_Text mainScoreDisplay;
	public TMP_Text mainLevelDisplay;
	public GameObject nextLevelButtons;
	public string nextLevelToLoad;

    Scene scene;
    
    private void Awake() {
        if (instance != null) {  // If not null or not equal to this instance
            Destroy(this.gameObject);   // Destroy the gameObject
        }
        else {
            instance = this.gameObject.GetComponent<GameManager>();;   // Otherwise the instance = this instance
        }
        mainScoreDisplay.text = "0";
        mainLevelDisplay.text = "0";
        scene = SceneManager.GetActiveScene();
        // inactivate the nextLevelButtons gameObject, if it is set
        if (nextLevelButtons)
            nextLevelButtons.SetActive (false);

        // DontDestroyOnLoad(this.gameObject);     // Makes sure that the game manager never gets destroyed
        itemManager = GetComponent<ItemManager>(); // Set the item manager component
        tileManager = GetComponent<TileManager>(); // Set the tile manager component 
        uiManager = GetComponent<UIManager>();
        player = FindObjectOfType<Player>();
    }

    public void AddPoints(int amount)
	{
		// increase score
		score+=amount;
		// update UI
		mainScoreDisplay.text = "BankAccount: " + score.ToString() + "  Goal: " + beatLevelScore;
        mainLevelDisplay.text = scene.name;
	}


    void Update() {
        if (canBeatLevel && (score == beatLevelScore))  // check to see if beat game
			BeatLevel();
    }

    void BeatLevel() {
		// activate the nextLevelButtons gameObject, if it is set 
        // if (NextLevelFunction)
		// 	NextLevelFunction.SetActive (true);
		if (nextLevelButtons)
			nextLevelButtons.SetActive (true);
	}

    // public function that can be called to go to the next level of the game
	public void NextLevel()
	{
		// we are just loading the specified next level (scene)
        SceneManager.LoadScene(nextLevelToLoad);
	}
}
