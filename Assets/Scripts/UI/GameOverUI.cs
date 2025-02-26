using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private GameEventListenerGameState gameStateListener;
    [SerializeField] private GameStateSO gameStateSO;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button returnMenuButton;

    private void Start()
    {
        if (gameStateListener != null)
            gameStateListener.AddResponse(ShowGameOver);

        restartButton.onClick.AddListener(RestartGame);
        returnMenuButton.onClick.AddListener(ReturnToMenu);

        Hide(); // Ensure it's hidden at the start
    }

    private void ShowGameOver(GameStateSO.State state)
    {
        Debug.Log("SHOW GAME OVER EVENT RAISED");
        if (state == GameStateSO.State.GameOver)
        {
            Show();
            recipesDeliveredText.text = gameStateSO.currentScore.ToString();
        }
        else
        {
            Hide();
        }
    }

    private void RestartGame()
    {
        GameManager.Instance.RestartGame();
        Hide(); // Hide UI after restarting
    }

    private void ReturnToMenu()
    {
        GameManager.Instance.ReturnToMenu();
        Hide(); // Hide UI after returning to the menu
    }

    private void Show()
    {
        gameObject.transform.localScale = Vector3.one;
    }

    private void Hide()
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
