using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameStateSO gameStateSO;
    [SerializeField] private GameStateChangedEvent gameStateChangedEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (gameStateSO == null)
        {
            Debug.LogError("GameStateSO is not assigned in GameManager!");
            return;
        }

        ResetGameState();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene") return;

        UpdateGameState();
    }

    private void UpdateGameState()
    {
        switch (gameStateSO.currentGameState)
        {
            case GameStateSO.State.WaitingToStart:
                gameStateSO.waitingToStartTimer -= Time.deltaTime;
                if (gameStateSO.waitingToStartTimer < 0f)
                {
                    ChangeGameState(GameStateSO.State.CountdownToStart);
                }
                break;

            case GameStateSO.State.CountdownToStart:
                gameStateSO.countdownToStartTimer -= Time.deltaTime;
                if (gameStateSO.countdownToStartTimer < 0f)
                {
                    ChangeGameState(GameStateSO.State.GamePlaying);
                }
                break;

            case GameStateSO.State.GamePlaying:
                gameStateSO.gamePlayingTimer -= Time.deltaTime;
                if (gameStateSO.gamePlayingTimer < 0f)
                {
                    ChangeGameState(GameStateSO.State.GameOver);
                }
                break;

            case GameStateSO.State.GameOver:
                if (gameStateSO.currentScore > gameStateSO.highScore)
                {
                    gameStateSO.highScore = gameStateSO.currentScore;
                }
                gameStateChangedEvent.Raise(GameStateSO.State.GameOver); // SO Event triggers UI & game logic
                break;
        }
    }

    private void ChangeGameState(GameStateSO.State newState)
    {
        gameStateSO.currentGameState = newState;
        gameStateChangedEvent.Raise(newState); // Triggers event for UI & systems
    }

    public void RestartGame()
    {
        ResetGameState();
        gameStateChangedEvent.Raise(GameStateSO.State.WaitingToStart);
        Loader.Load(Loader.Scene.SampleScene);
    }

    public void ReturnToMenu()
    {
        ResetGameState();
        gameStateChangedEvent.Raise(GameStateSO.State.WaitingToStart);
        Loader.Load(Loader.Scene.MainMenuScene);
    }

    private void ResetGameState()
    {
        gameStateSO.currentGameState = GameStateSO.State.WaitingToStart;
        gameStateSO.currentScore = 0;
        gameStateSO.waitingToStartTimer = 1f;
        gameStateSO.countdownToStartTimer = 3f;
        gameStateSO.gamePlayingTimer = 10f;
    }

}
