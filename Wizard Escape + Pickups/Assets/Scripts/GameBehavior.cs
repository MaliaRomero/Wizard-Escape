using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameBehavior : MonoBehaviour 
{
    public string labelText = "Collect all 4 f and win your freedom!";
    public int maxItems = 4;
    public Stack<string> lootStack = new Stack<string>();
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public int _itemsCollected = 0;
    private string _state;
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    public int gameStartScene;
    public string SceneNameWin;
    public string SceneNameLose;

    //public string State
    //{
    //    get { return _state; }
    //    set { _state = value; } 
    //}

    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;

            if (_itemsCollected >= maxItems)
            {
                labelText = "You've found all the items!";
                showWinScreen = true;
                Time.timeScale = 0f;
            }
            else
            {
                labelText = "Item found, only " + (maxItems - _itemsCollected)
                    + " more to go!";
            }
        }
    }
    private int _playerHP = 3;

    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (_playerHP <= 0)
            {
                Time.timeScale = 0;
                showLossScreen = true;
            }
        }
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new
            InventoryList<string>();
    }

    public void Initialize()
    {
        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior =
            player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        Debug.Log("Jump");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25), "Player Health: " + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25), "Items Collected: " + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50), labelText);

        if (showWinScreen == true)
        {
            SceneManager.LoadScene("Win");
        }

        if (showLossScreen == true)
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
