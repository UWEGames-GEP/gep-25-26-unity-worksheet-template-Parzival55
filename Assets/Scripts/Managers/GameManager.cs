using UnityEngine;

public enum GameState
{
    Playing,
    InventoryOpen,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CurrentState = GameState.Playing;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //GameManager and State Machine
    public void SetState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Playing:
                //Debug.Log("Game State: Playing");
                Time.timeScale = 1f;
                break;

            case GameState.InventoryOpen:
                //Debug.Log("Game State: Inventory Open");
                Time.timeScale = 1f;
                break;

            case GameState.Paused:
                //Debug.Log("Game State: Paused");
                Time.timeScale = 0f;
                break;
        }
    }
}